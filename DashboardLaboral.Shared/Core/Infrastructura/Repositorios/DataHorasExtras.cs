using DashboarLaboral.Core.Aplicacion.Constants;
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
    [IndicadorData("HorasExtras", "Empleados con Horas Extras")]
    public class DataHorasExtras : IData
    {
        private readonly IDataContext dataContext;
        public Expression<Func<Horario, bool>> Filtro { get; private set; }

        public DataHorasExtras(IDataContext dataContext)
        {
            this.dataContext = dataContext;

            Filtro = h => 
                (!h.Administrativo.HasValue || h.Administrativo == 0) 
                && ((h.Horasextras - (h.Horasdescontadas ?? 0)) > 0)
                && !h.OffPremise;
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {
            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();

            var valor = await Contar(fecha);
            var valorAuxiliar = await CalcularHoras(fecha);
            return new IndicadorModel
            {
                Id = 4,
                Tipo = IndicadorTipo.Extendido,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                Descripcion = "Horas generadas",
                Tooltip = "Colaboradores del nivel operativo que han generado horas extras en los últimos 30 días.",
                Clase = "text-media color-ayer num-top",
                ClaseAuxiliar = "text-media color-ayer num-top",
                Valor = valor,
                ValorAuxiliar = valorAuxiliar,
                DatosClase = this.GetType().Name,
                FormatValorAuxiliar = "n2"

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
            var data = dataContext.ObtenerData(false)
                .Where(Filtro);

            return await data.Where(h => h.Fecha.Date >= fecha.AddDays(DateConstant.FechaMenos30Dias).Date && h.Fecha.Date <= fecha.Date)
                        .OrderBy(h => h.Codigoempleado)
                        .GroupBy(h => h.Codigoempleado)
                        .Select(g => g.Key)
                        .CountAsync();
        }

        public async Task<decimal> CalcularHoras(DateTime fecha)
        {
            var data = dataContext.ObtenerData(false)
                .Where(Filtro);

            return await data.Where(h => h.Fecha.Date >= fecha.AddDays(-30).Date && h.Fecha.Date <= fecha.Date)
                .SumAsync(h => h.Horasextras);
        }

    }
}
