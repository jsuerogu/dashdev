using ClosedXML.Excel;
using DashboarLaboral.Core.Aplicacion;
using DashboarLaboral.Core.Aplicacion.Constants;
using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace DashboarLaboral.Controllers
{
    [Authorize(Roles = AccessRoles.Consultas)]
    public class ConsultasController : Controller
    {
        private readonly IDataContext dataContext;
        private readonly IFechaService fechaService;
        private readonly IServiceProvider serviceProvider;
        private readonly IWebHostEnvironment env;
        private readonly IMailService mailService;
        private readonly MailDevModel mailDevModel;

        public ConsultasController(IDataContext dataContext, IFechaService fechaService, IServiceProvider serviceProvider, IWebHostEnvironment env,
                IOptions<MailDevModel> mailDevModel, IMailService mailService)
        {
            this.dataContext = dataContext;
            this.fechaService = fechaService;
            this.serviceProvider = serviceProvider;
            this.mailDevModel = mailDevModel.Value;
            this.env = env;
            this.mailService = mailService;
        }

        public async Task<ActionResult> Tops(string empresa, string orden, string indicador, string departamento, string tipo, string colaborador)
        {
            var model = new ConsultaModel()
            {
                Empresas = dataContext.ObtenerEmpresas().Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Vicepresidencias = dataContext.ObtenerVicepresidencias(empresa).Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Departamentos = dataContext.ObtenerDepartamentos(empresa, orden).Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Indicadores = dataContext.ObtenerListaIndicadores().Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Filtro = new FiltroModel()
                {
                    Empresa = empresa,
                    Vicepresidencia = orden,
                    FechaInicial = tipo == "1" ? fechaService.Hoy.AddDays(DateConstant.FechaMenos30Dias).Date : fechaService.Hoy.Date,
                    FechaFinal = fechaService.Hoy.Date,
                    Indicador = indicador,
                    Departamento = departamento,
                    RangoFecha = (tipo == "1"),
                    Colaborador = colaborador
                },

                Data = null
            };

            if (!string.IsNullOrEmpty(indicador))
                model.Data = await model.Filtro.GenerarResultado(serviceProvider);

            return View("Index", model);
        }

        public async Task<IActionResult> Index(string empresa, string orden, string indicador, string rangoHora, string colaborador)
        {
            var model = new ConsultaModel()
            {
                Empresas = dataContext.ObtenerEmpresas().Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Vicepresidencias = dataContext.ObtenerVicepresidencias(empresa).Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Departamentos = dataContext.ObtenerDepartamentos(empresa, orden).Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Indicadores = dataContext.ObtenerListaIndicadores().Select(e => new SelectListItem(e.Display, e.Value)).ToList(),
                Filtro = new FiltroModel()
                {
                    Empresa = empresa,
                    Vicepresidencia = orden,
                    FechaInicial = fechaService.Hoy.Date,
                    FechaFinal = fechaService.Hoy.Date,
                    Indicador = indicador,
                    RangoHora = (rangoHora == "true"),
                    Colaborador = colaborador
                },

                Data = null
            };

            if (!string.IsNullOrEmpty(indicador))
            {
                if (indicador == "DataHorasExtras")
                {
                    model.Filtro.RangoFecha = true;
                    model.Filtro.FechaFinal = fechaService.Hoy.Date;
                    model.Filtro.FechaInicial = fechaService.Hoy.AddDays(DateConstant.FechaMenos30Dias).Date;
                }
                model.Data = await model.Filtro.GenerarResultado(serviceProvider);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CargarData(ConsultaDataRequestModel model)
        {
            var filtro = JsonConvert.DeserializeObject<FiltroModel>(model.ExtraData);


            model.Filtro = filtro;
            var response = await model.PaginateDataHorario(serviceProvider);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConsultaModel model)
        {
            model.Empresas = dataContext.ObtenerEmpresas().Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            model.Vicepresidencias = dataContext.ObtenerVicepresidencias(model.Filtro.Empresa).Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            model.Departamentos = dataContext.ObtenerDepartamentos(model.Filtro.Empresa, model.Filtro.Vicepresidencia).Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            model.Indicadores = dataContext.ObtenerListaIndicadores().Select(e => new SelectListItem(e.Display, e.Value)).ToList();
            model.Data = await model.Filtro.GenerarResultado(serviceProvider);

            return View(model);
        }

        [HttpPost]
        public async Task<FileResult> ExportExcel(ConsultaModel model)
        {
            XLWorkbook wb = new XLWorkbook();
            var worksheet = wb.GenerarEncabezado(env.ContentRootPath);

            var departamentos = await model.Filtro.GenerarResultado(serviceProvider);

            int fila = 4;
            foreach (var departamento in departamentos.Departamentos)
            {
                fila = worksheet.Titulos(departamento, fila + 1);
                model.Filtro.Departamento = departamento.Titulo;
                var data = await model.Filtro.DataHorario(serviceProvider);

                foreach (var item in data)
                {
                    ++fila;
                    worksheet.Data(item, fila);
                }
            }

            using (MemoryStream stream = new())
            {
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TablaDatos.xlsx");
            }
        }

        [HttpPost]
        public async Task<FileResult> ExportPdf(ConsultaModel model)
        {
            Document document = new Document();
            MemoryStream stream = new MemoryStream();

            PdfWriter write = PdfWriter.GetInstance(document, stream);

            document.SetPageSize(PageSize.A4.Rotate());
            document.Open();

            document.GenerarEncabezado(env.ContentRootPath);

            var departamentos = await model.Filtro.GenerarResultado(serviceProvider);

            int fila = 4;
            foreach (var departamento in departamentos.Departamentos)
            {
                var table = document.Titulos(departamento, fila);
                model.Filtro.Departamento = departamento.Titulo;
                var data = await model.Filtro.DataHorario(serviceProvider);
                foreach (var item in data)
                {
                    table.Data(item, ++fila);
                }
                document.Add(table);

            }

            document.Close();
            return File(stream.ToArray(), MediaTypeNames.Application.Pdf, "TablaDatos.pdf");
        }

        [HttpPost]
        public async Task<JsonResult> ExportMail(FiltroModel filtro)
        {
            var mailTemplate = new MailTemplate();

            string asunto = "Dashboard laboral – estatus de colaboradores";
            try
            {
                var supervisores = await filtro.ObtenerSupervisores(serviceProvider);
                foreach (var supervisor in supervisores)
                {
                    string body = "<div id='heading11' class='card-header' aria-expanded='false' aria-controls='accordion10'>" +
                    "Reciba un cordial saludo, <br> Ver a continuación el estatus de sus colaboradores" +
                    "</div>";

                    filtro.Departamento = "";
                    var departamentos = await filtro.GenerarResultadoSupervisor(serviceProvider, supervisor);
                    foreach (var departamento in departamentos.Departamentos)
                    {

                        filtro.Departamento = departamento.Titulo;

                        departamento.Data = await filtro.DataHorarioSupervisor(serviceProvider, supervisor);
                         
                        body += departamento.GenerateBodyMailFromDepartamento();

                    }

                    if (!string.IsNullOrEmpty(supervisor) || env.IsDevelopment())
                    {
                        if (env.IsDevelopment())
                        {
                            var mailMessage = new MailMessage()
                            {
                                Subject = $"{asunto} supervisor: {supervisor}",
                                IsBodyHtml = true,
                                Body = body,
                                Priority = MailPriority.Normal
                            };
                            mailMessage.To.Add(mailDevModel.MailDev);
                            mailTemplate.MailMessages.Add(mailMessage);
                        }
                        else
                        {
                            var mailMessage = new MailMessage()
                            {
                                Subject = asunto,
                                IsBodyHtml = true,
                                Body = body,
                                Priority = MailPriority.Normal
                            };

                            mailMessage.To.Add(supervisor);
                            mailTemplate.MailMessages.Add(mailMessage);
                        }
                    }
                }

                await mailService.SendMailAsync(mailTemplate, dataContext);

                return Json(new { Mensaje = "Se ha procesado la data, se estarán enviando los correos" });
            }
            catch (Exception ex)
            {
                //si ocurre un error regresamos false y el error
                string error = "Ocurrio un error: " + ex.Message;
                ViewData["mensaje"] = error;
                return Json(new { Mensaje = error });
            }
        }
    }
}
