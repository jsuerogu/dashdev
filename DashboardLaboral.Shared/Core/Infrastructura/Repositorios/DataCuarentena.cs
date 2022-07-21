using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Attributes;
using DashboarLaboral.Data;
using DashboarLaboral.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Repositorios
{
    [IndicadorData("Cuarentena", "Cuarentena")]
    public class DataCuarentena : IData
    {
        private readonly IDataContext dataContext;
        public Expression<Func<Horario, bool>> Filtro { get; private set; }

        public DataCuarentena(IDataContext dataContext, insitedb context)
        {
            this.dataContext = dataContext;

            Filtro = h => h.Ausentismo != null
            && h.Ausentismo.Cuarentena == 1;
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {
            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();
            return new IndicadorModel
            {
                Id = 3,
                Tipo = IndicadorTipo.Simple,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                DatosClase = GetType().Name,
                Tooltip = "Colaboradores fuera de las instalaciones por temas relacionados con COVID-19.",
                Clase = "text-media color-ayer num-top",
                Valor = await Contar(fecha),
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

            return await data.Where(h => h.Fecha.Date == fecha.Date)
                        .CountAsync();
        }
    }
}
