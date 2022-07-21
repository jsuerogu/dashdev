using DashboardLaboral.Shared.Core.Aplicacion.Contratos;
using DashboardLaboral.Shared.Core.Infrastructura.Services;
using DashboardLaboral.Shared.Models;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Mappers;
using DashboarLaboral.Core.Infrastructura.Services;
using DashboarLaboral.Data;
using DashboarLaboral.Models;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using System;
using System.Linq;
using System.Reflection;

namespace DashboarLaboral
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<insitedb>(config => 
                config.UseSqlServer(Configuration.GetConnectionString("connectionString")));


            services.AddAutoMapper(typeof(HorarioMapper).Assembly);
            services.AddTransient<IMailService, MailService>();
            services.AddDataIndicadores();
            services.AddRepositories();
            services.AddMediatR(GetType().Assembly);
            services.AddScoped<IGraficoRender, GraficoRender>();
            services.AddBackgroundJobsFromAssembly(typeof(IData).Assembly);
            services.AddScoped<ILogSql, LogSql>();
            services.AddHangfireServer((serviceProvider, options) =>
            {
                var backgroudJobQueues = serviceProvider.GetServices<IBackgroundJob>()
                        .Select(t => t.GetType())
                        .SelectMany(t => t.GetMethods().Where(m => Attribute.IsDefined(m, typeof(QueueAttribute))))
                        .Select(m => m.GetCustomAttribute<QueueAttribute>().Queue)
                        .ToArray();

                if(backgroudJobQueues != null && backgroudJobQueues.Length > 0)
                {
                    options.Queues = backgroudJobQueues;

                }
            });
            services.AddHangfire(config =>
                config.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("connectionString"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
     
            services.AddHangfireServer();

            services.Configure<MailDevModel>(Configuration.GetSection("MailDevModel"));
            services.Configure<ApexApiModel>(Configuration.GetSection("ApexApiModel"));
            services.Configure<RendeGraficosModel>(Configuration.GetSection("RendeGraficosModel")); 

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));


            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddControllersWithViews();

            services.AddFluentValidation(x =>
            {
                x.DisableDataAnnotationsValidation = true;
                x.ImplicitlyValidateChildProperties = true;

                x.RegisterValidatorsFromAssemblyContaining<HorarioMapper>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseHangfireDashboard();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseBackgroudJobs();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Start}/{action=Index}/{id?}");
            });
        }

    }
}
