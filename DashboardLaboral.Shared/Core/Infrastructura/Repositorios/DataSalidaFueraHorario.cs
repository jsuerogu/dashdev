using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Attributes;
using DashboarLaboral.Data;
using DashboarLaboral.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Repositorios
{
    [IndicadorData("SalidasFueraHorario", "Incumplimiento Horario")]
    public class DataSalidaFueraHorario : IData
    {
        private readonly IDataContext dataContext;
        private readonly DataPresentes dataPresentes;
        public Expression<Func<Horario, bool>> Filtro { get; private set; }

        public DataSalidaFueraHorario(IDataContext dataContext, DataPresentes dataPresentes, IFechaService fechaService, 
            insitedb context)
        {
            this.dataContext = dataContext;
            var currentDateTime = fechaService.FechaHora;

            Filtro =
                   h =>
                     h.Trabajahoy == 1
                    && !h.OffPremise &&
                    h.Ausentismo == null &&
                    h.Poncheentrada.HasValue
                    && h.Ponchesalida.HasValue
                    && (((EF.Functions.DateDiffMinute(h.RealHoraIni, h.Horafin) / (decimal)60.00) -
                            (h.Administrativo.HasValue && h.Administrativo.Value == 1
                                ? dataContext.HorasAlmuerzoAdm()
                                : 0)) >
                        (EF.Functions.DateDiffMinute(h.Poncheentrada, h.Ponchesalida) / (decimal)60.00) -
                            (h.HorasFuera.HasValue && h.HorasFuera.Value > 0
                                ? h.HorasFuera.Value
                                : 0));

            this.dataPresentes = dataPresentes;
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {
            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();

            var valor = await Contar(fecha);

            var presenteValor = await dataPresentes.Contar(fecha);
            var valorAuxiliar = presenteValor == 0 || valor == 0 ? 0
                : (int)Math.Round(((decimal)valor * 100) / (decimal)presenteValor);

            return new IndicadorModel
            {
                Id = 12,
                Tipo = IndicadorTipo.Extendido,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                Tooltip = "Colaboradores del nivel administrativo que salieron de las instalaciones antes de su hora de salida según horario en SAP.",
                Descripcion = "Del total asistencia",
                Clase = "text-media color-ayer num-top",
                ClaseAuxiliar = "text-media color-ayer num-top",
                Valor = valor,
                ValorAuxiliar = valorAuxiliar,
                DatosClase = this.GetType().Name,
                ExtendValorAuxiliar = "%"
            };
        }

        public Task<IQueryable<Horario>> ConsultaData(DateTime fechaInicial, DateTime fechaFinal, bool isHourFilter = true)
        {
            return Task.FromResult(dataContext.ObtenerData(isHourFilter)
                .Where(Filtro)
                .Where(h => h.Fecha.Date >= fechaInicial.Date && h.Fecha.Date <= fechaFinal.Date));
        }

        public async Task<int> Contar(DateTime fecha)
        {
            var data = dataContext.ObtenerData()
                .Where(Filtro)
                .Where(h => h.Fecha.Date == fecha.Date);
            return await data
                            .OrderBy(h => h.Codigoempleado)
                            .GroupBy(h => h.Codigoempleado)
                            .Select(g => g.Key)
                            .CountAsync();
        }
    }
}
