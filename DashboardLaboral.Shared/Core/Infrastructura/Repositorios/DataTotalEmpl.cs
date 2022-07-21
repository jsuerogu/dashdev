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
    [IndicadorData("TotalEmpl", "Empleados Activos")]
    public class DataTotalEmpl : IData
    {
        private readonly IDataContext dataContext;
        public Expression<Func<Horario, bool>> Filtro { get; private set; }

        public DataTotalEmpl(IDataContext dataContext)
        {
            this.dataContext = dataContext;
            Filtro = h => true;
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {
            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();

            var valor = await Contar(fecha);
            var valorAuxiliar =  valor - await Contar(fecha.Ayer());

            return new IndicadorModel
            {
                Id = 14,
                Tipo = IndicadorTipo.Simple,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                Tooltip = "Cantidad de empleados activos en el Grupo SID.",
                Descripcion = "Comparado con ayer",
                Clase = "text-media color-hoy num-top",
                ClaseAuxiliar = "text-media color-hoy num-top",
                Valor = valor,
                ValorAuxiliar = valorAuxiliar,
                DatosClase = this.GetType().Name
            };
        }

        public Task<IQueryable<Horario>> ConsultaData(DateTime fechaInicial, DateTime fechaFinal, bool isHourFilter)
        {
            return Task.FromResult(dataContext.ObtenerData(false)
                .Where(Filtro)
                .Where(h => h.Fecha.Date >= fechaInicial.Date && h.Fecha.Date <= fechaFinal.Date));
        }

        public async Task<int> Contar(DateTime fecha)
        {
            var data = dataContext.ObtenerData(false)
                .Where(Filtro);

            return await data.Where(h => h.Fecha.Date == fecha.Date).CountAsync();
        }

        public async Task<int> ContarOperativos(DateTime fecha)
        {
            var data = dataContext.ObtenerData()
                .Where(Filtro);

            return await data.Where(h => h.Fecha.Date == fecha.Date && (!h.Administrativo.HasValue || h.Administrativo == 0))
                        .CountAsync();
        }

        public async Task<int> ContarAdministrativos(DateTime fecha)
        {
            var data = dataContext.ObtenerData()
                .Where(Filtro);

            return await data.Where(h => h.Fecha.Date == fecha.Date && (h.Administrativo.HasValue && h.Administrativo == 1))
                        .CountAsync();
        }

    }
}
