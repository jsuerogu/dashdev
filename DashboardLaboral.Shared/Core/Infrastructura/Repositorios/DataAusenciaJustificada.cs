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
    [IndicadorData("AusenciaJust", "Ausencia Justificada", 3)]
    public class DataAusenciaJustificada : IData
    {
        private readonly IDataContext dataContext;
        public Expression<Func<Horario, bool>> Filtro { get; private set; }

        public DataAusenciaJustificada(IDataContext dataContext)
        {
            this.dataContext = dataContext;

            Filtro = h => h.Ausentismo != null && h.Ausentismo.Aujus
                          && !h.OffPremise
                          &&  h.Ausentismo.Riesgo != 1
                          && h.Ausentismo.Cuarentena != 1;
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {

            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();

            var valor = await Contar(fecha);
            return new IndicadorModel
            {
                Id = 1,
                Tipo = IndicadorTipo.Simple,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                DatosClase = GetType().Name,
                Tooltip = "Colaboradores que no están trabajando porque tienen una ausencia justificada como: licencia médica, vacaciones, y viajes.",
                Clase = "text-media color-ayer num-top",
                Valor = valor
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
