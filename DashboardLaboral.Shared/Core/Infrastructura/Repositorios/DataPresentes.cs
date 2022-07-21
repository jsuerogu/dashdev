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
    [IndicadorData("Presentes", "A tiempo", 4)]
    public class DataPresentes : IData
    {
        private readonly IDataContext dataContext;
        private readonly DataOnPremise dataOnPremise;
        public Expression<Func<Horario, bool>> Filtro { get; private set; }

        public DataPresentes(IDataContext dataContext, DataOnPremise dataOnPremise)
        {
            this.dataContext = dataContext;
            Filtro = h => h.Trabajahoy == 1
                && (h.Horaini.HasValue && h.Poncheentrada <= h.RealHoraIni.Value.AddMinutes(dataContext.MinutosMaxTardanza()))
                && h.Poncheentrada != null && h.Ausentismo == null
                && !h.OffPremise;


            this.dataOnPremise = dataOnPremise;
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {
            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();

            var onPremiseValor = await dataOnPremise.Contar(fecha);

            var valor = await Contar(fecha);
            var valorAuxiliar = onPremiseValor == 0 || valor == 0 ? 0
                : (int)Math.Round(((decimal)valor * 100) / (decimal)onPremiseValor);

            return new IndicadorModel
            {
                Id = 11,
                Tipo = IndicadorTipo.Extendido,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                Tooltip = "Colaboradores laborando presencialmente en las instalaciones del Grupo SID.",
                Descripcion = "Del total On-Premise",
                Clase = "text-media color-ayer num-top",
                ClaseAuxiliar = "text-media color-ayer num-top",
                Valor = valor,
                ValorAuxiliar = valorAuxiliar,
                ExtendValorAuxiliar = "%",
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

            return await data.Where(h => h.Fecha.Date == fecha.Date).CountAsync(); ;
        }
    }
}
