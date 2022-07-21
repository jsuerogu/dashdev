using AutoMapper;
using DashboardLaboral.Shared.Core.Aplicacion.Enums;
using DashboardLaboral.Shared.Models;
using DashboarLaboral.Core.Infrastructura.Repositorios;
using DashboarLaboral.Data;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DashboardLaboral.Shared.Core.Aplicacion.Mappers.Converters
{
    public class IndicadorConverter : IValueResolver<Horario, HorarioModel, IndicadorDetailModel>
    {
        private readonly IServiceProvider serviceProvider;

        public IndicadorConverter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IndicadorDetailModel Resolve(Horario source, HorarioModel destination, IndicadorDetailModel destMember, ResolutionContext context)
        {
            var notocatrabajar = serviceProvider.GetService<DataNoTocaTrabajar>();
            var ausenciajust = serviceProvider.GetService<DataAusenciaJustificada>();
            var inasistencias = serviceProvider.GetService<DataInasistencias>();
            var offpremise = serviceProvider.GetService<DataOffPremise>();
            var presentes = serviceProvider.GetService<DataPresentes>();
            var tardanzas = serviceProvider.GetService<DataTardanzas>();
            
            var indicador = "";
            var tipo = "";
            try
            {
                if (notocatrabajar.Filtro.Compile().Invoke(source))
                    indicador = IndicadoresEnum.NoTocaTrabajar;
                else if (ausenciajust.Filtro.Compile().Invoke(source))
                    indicador = IndicadoresEnum.AusenciaJustificada;
                else if (inasistencias.Filtro.Compile().Invoke(source))
                    indicador = IndicadoresEnum.Inasistencias;
                else if (offpremise.Filtro.Compile().Invoke(source))
                    indicador = IndicadoresEnum.OffPremise;
                else if (presentes.Filtro.Compile().Invoke(source))
                    indicador = IndicadoresEnum.Presentes;
                else if (tardanzas.Filtro.Compile().Invoke(source))
                    indicador = IndicadoresEnum.Tardanzas;

            }
            catch (Exception)
            {

                throw;
            }

            tipo = indicador.ConvertindicadorToTipo(source.Tipo2);

            return new IndicadorDetailModel() { Indicador = indicador, Tipo = tipo};
        }
    }
}
