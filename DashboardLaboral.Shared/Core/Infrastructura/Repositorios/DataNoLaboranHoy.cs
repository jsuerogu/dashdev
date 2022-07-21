using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Attributes;
using DashboarLaboral.Data;
using DashboarLaboral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Repositorios
{
    [IndicadorData("NoLaboranHoy", "No Laboran")]
    public class DataNoLaboranHoy : IData
    {
        private readonly IDataContext dataContext;
        private readonly IHttpContextAccessor httpContext;

        public Expression<Func<Horario, bool>> Filtro { get; private set; }

        public DataNoLaboranHoy(IDataContext dataContext, IHttpContextAccessor httpContext, insitedb context)
        {
            this.dataContext = dataContext;
            Filtro = h => h.Trabajahoy == 0
                    || (h.Ausentismo != null
                        && (h.Ausentismo.Aujus
                        || h.Ausentismo.Riesgo == 1
                        || h.Ausentismo.Cuarentena == 1));

            this.httpContext = httpContext;
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {
            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();

            return new IndicadorModel
            {
                Id = 7,
                Tipo = IndicadorTipo.Simple,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                Tooltip = "Colaboradores a los que no les corresponde trabajar HOY según horario en SAP o que cuentan con una ausencia justificada.",
                Clase = "text-media color-hoy num-top",
                Valor = await Contar(fecha),
                DatosClase = this.GetType().Name
            };
        }

        public Task<IQueryable<Horario>> ConsultaData(DateTime fechaInicial, DateTime fechaFinal, bool isHourFilter)
        {
            return Task.FromResult(dataContext.ObtenerData(isHourFilter)
                .Where(Filtro)
                .Where(h => h.Fecha.Date >= fechaInicial.Date && h.Fecha.Date <= fechaFinal.Date));
        }

        public async Task<int> Contar(DateTime fecha)
        {
            var data = dataContext.ObtenerData()
                .Where(Filtro);

            return await data.Where(h => h.Fecha.Date == fecha.Date).CountAsync();
        }
    }
}
