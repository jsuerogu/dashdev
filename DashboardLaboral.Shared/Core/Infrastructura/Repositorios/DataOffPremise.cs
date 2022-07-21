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
    [IndicadorData("OffPremise", "Off-Premise", 5)]
    public class DataOffPremise : IData
    {
        private readonly IDataContext dataContext;
        public Expression<Func<Horario, bool>> Filtro { get; private set; }

        public DataOffPremise(IDataContext dataContext, insitedb context)
        {
            this.dataContext = dataContext;

            Filtro = h => h.Trabajahoy == 1
                        && (h.Ausentismo == null)
                        && h.OffPremise;
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {
            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();

            return new IndicadorModel
            {
                Id = 9,
                Tipo = IndicadorTipo.Simple,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                Tooltip = "Colaboradores que les corresponde trabajar fuera de las instalaciones del Grupo SID.",
                Clase = "text-media color-ayer num-top",
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
