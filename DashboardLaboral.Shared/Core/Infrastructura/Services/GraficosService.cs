using DashboarLaboral.Core.Aplicacion.Constants;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Infrastructura.Repositorios;
using DashboarLaboral.Data;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using DashboarLaboral.Models.Graficos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Services
{
    public class GraficosService : IGraficosService
    {

        private readonly DataOnPremise dataOnPromise;
        private readonly DataPresentes dataPresentes;
        private readonly DataInasistencias dataInasistencias;
        private readonly DataTardanzas dataTardanzas;
        private readonly DataTotalEmpl dataTotalEmpl;

        private readonly IDataContext dataContext;
        private readonly IFechaService fechaService;
        private readonly IHttpContextAccessor httpContext;


        public GraficosService(DataOnPremise dataOnPromise, DataPresentes dataPresentes, DataInasistencias dataInasistencias, DataTardanzas dataTardanzas, IDataContext dataContext, DataTotalEmpl dataTotalEmpl, IFechaService fechaService, IHttpContextAccessor httpContext)
        {
            this.dataOnPromise = dataOnPromise;
            this.dataPresentes = dataPresentes;
            this.dataInasistencias = dataInasistencias;
            this.dataTardanzas = dataTardanzas;
            this.dataContext = dataContext;
            this.dataTotalEmpl = dataTotalEmpl;
            this.fechaService = fechaService;
            this.httpContext = httpContext;
        }

        public async Task<RadialBarModel> ObtenerRadialBarDatos(DateTime fechaHoy)
        {
            decimal presenteHoyPorc = await CalcularPresentesHoy(fechaHoy);
            decimal presenteAyerPorc = await CalcularPresentesAyer(fechaHoy);

            var ausenteHoyPorc = presenteHoyPorc > 0 ? 100 - presenteHoyPorc
                : presenteHoyPorc;

            var ausenteAyerPorc = presenteAyerPorc > 0 ? 100 - presenteAyerPorc
                : presenteAyerPorc;

            return new RadialBarModel
            {
                PresentesHoy = presenteHoyPorc,
                AusentesHoy = ausenteHoyPorc,
                PresentesAyer = presenteAyerPorc,
                AusentesAyer = ausenteAyerPorc
            };
        }

        private async Task<decimal> CalcularPresentesHoy(DateTime fechaHoy)
        {
            IQueryable<Horario> onPremiseHoy = await dataOnPromise.ConsultaData(fechaHoy, fechaHoy, true);
            IQueryable<Horario> presentesHoy = await dataPresentes.ConsultaData(fechaHoy, fechaHoy, true);

            var presenteHoyCount = (decimal)presentesHoy.Count();
            var onPremiseHoyCount = (decimal)onPremiseHoy.Count();

            decimal presenteHoyPorce = presenteHoyCount == 0 || onPremiseHoyCount == 0 ? 0
                : Math.Round((presenteHoyCount
                        / onPremiseHoyCount)
                        * 100);

            return presenteHoyPorce;
        }

        private async Task<decimal> CalcularPresentesAyer(DateTime fechaHoy)
        {
            IQueryable<Horario> onPremiseAyer = await dataOnPromise.ConsultaData(fechaHoy.Ayer(), fechaHoy.Ayer(), false);
            IQueryable<Horario> presentesAyer = await dataPresentes.ConsultaData(fechaHoy.Ayer(), fechaHoy.Ayer(), false);

            var presenteAyerCount = (decimal)presentesAyer.Count();
            var onPremiseAyerCount = (decimal)onPremiseAyer.Count();

            decimal presenteAyerPorce = presenteAyerCount == 0 || onPremiseAyerCount == 0 ? 0
                : Math.Round((presenteAyerCount
                        / onPremiseAyerCount)
                        * 100);

            return presenteAyerPorce;
        }
        public async Task<HorasExtrasChart> ObtenerGraficoHorasExtras(DateTime fechaIni, DateTime fechaFin, int? administrativo, string empresa = null, string vicepresidencia = null, string depatamento = null)
        {
            IQueryable<Horario> data = await dataTotalEmpl.ConsultaData(fechaIni, fechaFin, false);

            empresa ??= httpContext.ObtenerEmpresa();
            vicepresidencia ??= httpContext.ObtenerVicePresidencia();
            administrativo ??= httpContext.ObtenerColaborador();

            data.Where(h => 
                    (empresa == null || h.Empresa.Equals(empresa))
                    && (vicepresidencia == null || h.Vicepresidencia.Equals(vicepresidencia))
                    && (administrativo < 0 || h.Vicepresidencia.Equals(administrativo)));

            var result = new HorasExtrasChart() 
            { 
                MesAno = $"{fechaFin.Month} - {fechaFin.Year}",
                HorasExtras = data.Sum(h => h.Horasextras),
                HorasExtrasPlanificadas = data.Sum(h => h.HorasextrasPla)
            };

            return result;
        }

        public async Task<AreaChartModel> ObtenerAreaChartDatos(DateTime fechaHoy)
        {
            var dataInasistencia = await dataInasistencias.ContarUltimos30Dias(fechaHoy);

            var dataTardanza = await dataTardanzas.ContarUltimos30Dias(fechaHoy);

            List<DateTime> fechas = new();

            for (DateTime i = fechaHoy.AddDays(DateConstant.FechaMenos30Dias); i <= fechaHoy; i = i.AddDays(1))
            {
                fechas.Add(i);
                if (!dataInasistencia.Any(a => a.Fecha.Date == i.Date))
                    dataInasistencia.Add(new AreaChartSerieModel { Fecha = i.Date, Total = 0 });
                if (!dataTardanza.Any(a => a.Fecha.Date == i.Date))
                    dataTardanza.Add(new AreaChartSerieModel { Fecha = i.Date, Total = 0 });
            }

            var model = new AreaChartModel()
            {
                Fechas = fechas.OrderBy(f => f).ToList(),
                Tardanzas = dataTardanza.OrderBy(i => i.Fecha).ToList(),
                Inasistencias = dataInasistencia.OrderBy(i => i.Fecha).ToList()
            };

            return model;
        }
    }
}
