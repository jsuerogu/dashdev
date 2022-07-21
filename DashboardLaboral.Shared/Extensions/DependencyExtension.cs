using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Attributes;
using DashboarLaboral.Core.Infrastructura.Repositorios;
using DashboarLaboral.Core.Infrastructura.Repositorios.CRUD;
using DashboarLaboral.Core.Infrastructura.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyExtension
    {
        public static object ResolveScope(this IServiceProvider serviceProvider, Type serviceType)
        {
            var scope = serviceProvider.CreateScope();
            return scope.ServiceProvider.GetService(serviceType);
        }

        public static TService ResolveScope<TService>(this IServiceProvider serviceProvider)
        {
            return (TService)serviceProvider.ResolveScope(typeof(TService));
        }

        private static IServiceCollection LocateIndicadores(this IServiceCollection services)
        {
            Dictionary<string, IndicadorDataAttribute> indicadores = new();
            Assembly.GetExecutingAssembly()
               .DefinedTypes.Where(t => Attribute.IsDefined(t, typeof(IndicadorDataAttribute)))
               .ToList()
               .ForEach(indicador =>
               {
                   if(!indicadores.ContainsKey(indicador.Name))
                   {
                       var attribute = indicador.GetCustomAttribute<IndicadorDataAttribute>();
                       attribute.DataType = indicador;
                       indicadores.Add(indicador.Name, attribute);
                   }
                   services.AddTransient(indicador);
               });
            services.AddSingleton(indicadores);
            return services;
        }


        public static IServiceCollection AddDataIndicadores(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddTransient<IDataContext, DataContext>();
            services.AddTransient<IFechaService, FechaService>();
            services.AddTransient<IGraficosService, GraficosService>();
            services.AddTransient<IIndicadoresService, IndicadoresService>();
            services.AddTransient<ITopService, TopService>();

            return services.LocateIndicadores();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryEmpresa, RepositoryEmpresa>();
            services.AddTransient<IRepositoryParametro, RepositoryParametro>();
            services.AddTransient<IRepositoryAusentismo, RepositoryAusentismo>();
            services.AddTransient<IRepositoryParametroCorreos, RepositoryParametroCorreos>();
            services.AddTransient<IRepositoryPosicionOffPremise, RepositoryPosicionOffPremise>();
            services.AddTransient<IRepositoryPosicionesFeriados, RepositoryPosicionesFeriados>();
            return services;
        }
        
    }
}
