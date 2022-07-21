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
    [IndicadorData("AsistenciaNoJustificada", "Asistencias No Justificadas")]
    public class DataAsistenciaNoJustificada : IData
    {
        private readonly IDataContext dataContext;
        private readonly DataOffPremise dataOffPremise;

        public DataAsistenciaNoJustificada(IDataContext dataContext, DataOffPremise dataOffPremise
            , insitedb context)
        {
            Filtro = h => h.Trabajahoy == 0
              && !h.OffPremise
              && (h.Poncheentrada.HasValue || h.Ponchesalida.HasValue);

            this.dataContext = dataContext;
            this.dataOffPremise = dataOffPremise;
        }

        public Expression<Func<Horario, bool>> Filtro { get; set; }

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
                        .CountAsync();
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {
            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();

            var totalOffPremise = await dataOffPremise.Contar(fecha);

            var valor = await Contar(fecha);

            var valorAuxiliar = totalOffPremise == 0 || valor == 0 ? 0
                : (int)Math.Round(((decimal)valor * 100) / (decimal)totalOffPremise);

            return new IndicadorModel
            {
                Id = 16,
                Tipo = IndicadorTipo.Extendido,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                Tooltip = "Colaboradores con Ausencia Justificada o que no les toque trabajar y que se presentan a laborar a las instalaciones del Grupo SID.",
                Descripcion = "Del total Off-Premise",
                Clase = "text-media color-ayer num-top",
                ClaseAuxiliar = "text-media color-ayer num-top",
                Valor = valor,
                ValorAuxiliar = valorAuxiliar,
                ExtendValorAuxiliar = "%",
                DatosClase = this.GetType().Name
            };
        }
    }
}
