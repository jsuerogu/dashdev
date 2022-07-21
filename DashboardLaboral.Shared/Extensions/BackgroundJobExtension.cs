using DashboarLaboral.Core.Aplicacion.Contratos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BackgroundJobExtension
    {
        public static IServiceCollection AddBackgroundJobsFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            var backgroundJobs = assemblies.SelectMany(a => a.DefinedTypes.Where(t =>
                typeof(IBackgroundJob).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract));


            backgroundJobs
                .ToList()
                .ForEach(backgroundJob =>
                {
                    services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IBackgroundJob), backgroundJob));
                });

            return services;
        }

        public static IServiceCollection AddBackgroundJobsFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            return services.AddBackgroundJobsFromAssemblies(assembly);
        }

        public static IApplicationBuilder UseBackgroudJobs(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var jobs = scope.ServiceProvider.GetServices<IBackgroundJob>()
                .ToList();
                jobs.ForEach(backgroundJob => backgroundJob.Prepare());
            return app;
        }
    }
}
