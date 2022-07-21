using DashboarLaboral.Core.Aplicacion.Constants;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Attributes;
using DashboarLaboral.Data;
using DashboarLaboral.Models;
using DashboarLaboral.Models.Graficos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Repositorios
{
    [IndicadorData("Inasistencias", "Inasistencias", 1)]
    public class DataInasistencias : IData
    {
        private readonly IDataContext dataContext;
        private readonly DataOnPremise dataOnPremise;
        private readonly DataPresentes dataPresentes;

        public Expression<Func<Horario, bool>> Filtro { get; private set; }

        public DataInasistencias(IDataContext dataContext, DataOnPremise dataOnPremise, DataPresentes dataPresentes,
            insitedb context)
        {
            this.dataContext = dataContext;

            Filtro = h => h.Trabajahoy == 1
                        && h.Horaini.HasValue && h.Poncheentrada == null
                        && (h.Ausentismo == null || !h.Ausentismo.Aujus)
                        && !h.OffPremise;

            this.dataOnPremise = dataOnPremise;
            this.dataPresentes = dataPresentes;
        }

        public async Task<IndicadorModel> ObtenerModel(DateTime fecha)
        {
            var attribute = GetType().GetCustomAttribute<IndicadorDataAttribute>();

            var onPremiseValor = await dataOnPremise.Contar(fecha);
            var presentesValor = await dataPresentes.Contar(fecha);
            
            var valor = await Contar(fecha);
            var valorAuxiliar = onPremiseValor == 0 || valor == 0 ? 0
                : (int)Math.Round(((decimal)valor * 100) / (decimal)onPremiseValor);

            return new IndicadorModel
            {
                Id = 5,
                Tipo = IndicadorTipo.Extendido,
                Nombre = attribute.Nombre,
                Titulo = attribute.Titulo,
                Tooltip = "Colaboradores que debieron presentarse a laborar en las instalaciones del Grupo SID y no lo hicieron.",
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

            
            return await data.Where(h => h.Fecha.Date == fecha.Date).CountAsync();
        }

        public async Task<List<AreaChartSerieModel>> ContarUltimos30Dias(DateTime fecha)
        {
            var data = (await ConsultaData(fecha.AddDays(DateConstant.FechaMenos30Dias), fecha, false))
                .Where(i => i.Administrativo == 0);

                return data
                    .GroupBy(h => h.Fecha.Date)
                    .Select(g => new AreaChartSerieModel { Fecha = g.Key, Total = g.Count() })
                    .OrderBy(l => l.Fecha)
                    .ToList();

        }

    }
}
