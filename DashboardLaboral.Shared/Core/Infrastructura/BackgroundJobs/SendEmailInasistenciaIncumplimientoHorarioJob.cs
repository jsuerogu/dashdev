using DashboardLaboral.Shared.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;


namespace DashboarLaboral.Core.Infrastructura.BackgroundJobs
{
    public class SendEmailInasistenciaIncumplimientoJob : IBackgroundJob
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IDataContext dataContext;
        private readonly IMailService mailService;
        private readonly IFechaService fechaService;
        private readonly ILogSql log;
        private readonly IGraficoRender graficoRender;

        private static bool IsProcessing = false;

        public SendEmailInasistenciaIncumplimientoJob(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            mailService = serviceProvider.ResolveScope<IMailService>();
            dataContext = serviceProvider.ResolveScope<IDataContext>();
            fechaService = serviceProvider.ResolveScope<IFechaService>();
            log = serviceProvider.ResolveScope<ILogSql>();
            this.graficoRender = serviceProvider.ResolveScope<IGraficoRender>();
        }

        [Queue("sendemail")]
        [AutomaticRetry(Attempts = 0)]
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            if (!IsProcessing)
            {
                try
                {

                    string fechaEjecucion = fechaService.FechaEjecucion().ToString("dd/MM/yyyy hh:mm:ss");
                    var parametrosCorreo = dataContext.ObtenerParametrosCorreo().ToList();
                    MailTemplate mailTemplate = new MailTemplate();
                    IsProcessing = true;
                    string asunto = $"{DateTime.Now.FechaZonaHoraria().ToShortDateString()} - Reporte Inasistencia e Incumplimiento Horario de Personal";
                    var listadoIndicadores = dataContext.ObtenerListaIndicadores();
                    List<string> filtros = new();

                    int countData = 0;
                    foreach (var parametroCorreo in parametrosCorreo)
                    {
                        if (!parametroCorreo.Destinatario.IsCorrectEmail())
                        {
                            log.Error($"Error enviando correo automático: Destinatario {parametroCorreo.Destinatario} no tiene una dirección de correo valida", null);
                            continue;
                        }


                        countData = 0;
                        string body = "<div id='heading11' class='card-header' aria-expanded='false' aria-controls='accordion10'>" +
                        "Reciba un cordial saludo, <br> Ver a continuación el estatus de sus colaboradores" +
                        "</div>";


                        var filtro = new FiltroModel()
                        {
                            FechaInicial = fechaService.Hoy,
                            FechaFinal = fechaService.Hoy
                        };

                        foreach (var empresa in parametroCorreo.Empresas)
                        {
                            var bodyEmpresa = "";
                            filtro.Empresa = empresa.Empresa;

                            // grafico

                            var detalleEmpresa = dataContext.ObtenerDetalleEmpresa(empresa.Empresa);

                            if (detalleEmpresa == null)
                            {
                                detalleEmpresa = new()
                                {
                                    CodigoEmpresa = empresa.Empresa,
                                    Empresa1 = "Empresa no definida",
                                    Color = "red"
                                };
                            }

                            foreach (var vicepresidencia in empresa.VicePresidencias)
                            {
                                filtro.Vicepresidencia = vicepresidencia.Vicepresidencia;

                                foreach (var departamento in vicepresidencia.Departamentos)
                                {
                                    filtro.SearchValue = "";
                                    filtro.Departamento = departamento;
                                    filtro.Colaborador = "1";
                                    filtro.OrderDirection = "asc";
                                    var grafico = await graficoRender.GetBase64(filtro);

                                    bodyEmpresa += $@"           
                                             <table border = '0'>
                                              <tr>";

                                    if (!string.IsNullOrEmpty(grafico.GraficoInasistencia))
                                    {
                                        bodyEmpresa += $@"<td><div>
                                                                <img src=""data:image/png;base64,{grafico.GraficoInasistencia}"" width=""300"" height=""200"" />
                                                              </div></td>";
                                    }
                                    if (!string.IsNullOrEmpty(grafico.GraficoTardanza))
                                    {
                                        bodyEmpresa += $@"<td><div>
                                                                <img src=""data:image/png;base64,{grafico.GraficoTardanza}"" width=""300"" height=""200"" />
                                                              </div></td>";
                                    }
                                    if (!string.IsNullOrEmpty(grafico.Graficoincumpolimiento))
                                    {
                                        bodyEmpresa += $@"<td><div>
                                                                <img src=""data:image/png;base64,{grafico.Graficoincumpolimiento}"" width=""300"" height=""200"" />
                                                              </div></td>";
                                    }

                                    bodyEmpresa += $@"</tr><tr>";

                                    if (!string.IsNullOrEmpty(grafico.GraficoHorasExtras))
                                    {
                                        bodyEmpresa += $@"<td colspan='3'><div>
                                                                <img src=""data:image/png;base64,{grafico.GraficoHorasExtras}"" width=""300"" height=""200"" />
                                                              </div></td>";

                                    }
                                    bodyEmpresa += $@"</tr>
                                            </table>";

                                    foreach (var indicador in parametroCorreo.Indicadores.Split(',', StringSplitOptions.TrimEntries))
                                    {
                                        filtro.Indicador = indicador;
                                        var filtroStr = JsonConvert.SerializeObject(filtro);

  
                                        var data = await filtro.DataHorario(serviceProvider);

                                        countData += data.Count;

                                        if (data.Count > 0)
                                        {
                                            if (string.IsNullOrEmpty(bodyEmpresa))
                                                bodyEmpresa += $"<div style='background-color: {detalleEmpresa.Color}; color: white'><h2>({empresa.Empresa}){detalleEmpresa.Empresa1} - {vicepresidencia.Vicepresidencia}</h2></div>";

                                            bodyEmpresa += $"<div style='background-color: {detalleEmpresa.Color}; color: white'><h4>{listadoIndicadores.FirstOrDefault(i => i.Value == indicador)?.Display}</h4></div>";
                                            bodyEmpresa += new DepartamentoModel() { Data = data, Titulo = departamento, Total = data.Count }.GenerateBodyMailFromDepartamento();
                                        }
                                        filtros.Add($"[{parametroCorreo.Destinatario}]{filtro}: registros ({data.Count})");
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(bodyEmpresa))
                            {
                                body += bodyEmpresa;
                                body += "<br /><br /><br />";
                            }
                        }
                        body += "<br /><br /><br />";


                        if (countData > 0)
                        {
                            var mailMessage = new MailMessage()
                            {
                                Subject = asunto,
                                IsBodyHtml = true,
                                Body = body,
                                Priority = MailPriority.Normal
                            };

                            mailMessage.To.Add(parametroCorreo.Destinatario);

                            mailTemplate.MailMessages.Add(mailMessage);
                        }
                    }

                    log.Information($"Ultima Actualización: {fechaEjecucion} : {asunto}");

                    filtros.ForEach(filtro =>
                    {
                        log.Information(filtro);
                    });


                    if (mailTemplate.MailMessages.Count > 0)
                    {
                        await mailService.SendMailAsync(mailTemplate, dataContext);
                        log.Information($"Se enviaron {mailTemplate.MailMessages.Count} correos!");
                    }
                    else
                    {
                        log.Information($"No se encontro data para correo ({asunto})");
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"Error enviando correo automático: {ex.Message}", ex);
                    //throw;
                }
                finally
                {
                    log.Commit();
                    IsProcessing = false;
                }
            }
        }

        public void Prepare()
        {
            RecurringJob.RemoveIfExists("SendEmailInasistenciaIncumplimientoJob.ExecuteAsync");
            PurgeQueue("sendemail");

            using var scope = serviceProvider.CreateScope();

            var config = scope.ServiceProvider.GetService<IConfiguration>();

            if (Convert.ToBoolean(config["JobCorreo"]))
            {
                var schedule = dataContext.ObtenerParametros(GetType().Name);

                RecurringJob.AddOrUpdate(() => ExecuteAsync(new()), /*schedule.Valor*/ Cron.Minutely(), TimeZoneInfo.FindSystemTimeZoneById("SA Western Standard Time"), "sendemail");
            }
        }


        public void PurgeQueue(string queueName)
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }

            var toDelete = new List<string>();
            var monitor = JobStorage.Current.GetMonitoringApi();

            var queue = monitor.Queues().FirstOrDefault(x => x.Name == queueName);
            if (queue != null)
            {
                for (var i = 0; i < Math.Ceiling(queue.Length / 1000d); i++)
                {
                    monitor.EnqueuedJobs(queue.Name, 1000 * i, 1000)
                        .ForEach(x => toDelete.Add(x.Key));
                }

                for (var i = 0; i < Math.Ceiling(queue.Fetched.HasValue ? queue.Fetched.Value / 1000d : 0d); i++)
                {
                    monitor.FetchedJobs(queue.Name, 1000 * i, 1000)
                        .ForEach(x => toDelete.Add(x.Key));
                }

                foreach (var jobId in toDelete)
                {
                    BackgroundJob.Delete(jobId);
                }
            }
        }
    }
}
