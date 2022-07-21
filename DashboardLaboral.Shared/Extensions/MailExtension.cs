using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DashboarLaboral.Extensions
{
    public static class MailExtension
    {
        public record DataMail(SmtpClient Smtp, MailAddress FromAddress);

        public static bool IsCorrectEmail(this string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.Match(email).Success;
        }

        public static DataMail CreateSmtpClient(this IDataContext dataContext)
        {
            string MailAddress = "info@cikersc.com";
            string Host = "hgws24.win.hostgator.com";
            int Port = 25;
            string user = "info@cikersc.com";
            string pass = "Santa456258";
            bool ssl = false;


            MailAddress = dataContext.ObtenerParametros("MAIL_FROM")?.Valor ?? "";
            Host = dataContext.ObtenerParametros("MAIL_HOST")?.Valor ?? "";
            Port = dataContext.ObtenerParametros("MAIL_PORT") != null
                ? int.Parse(dataContext.ObtenerParametros("MAIL_PORT").Valor)
                : 25;
            user = dataContext.ObtenerParametros("MAIL_USER")?.Valor ?? "";
            pass = dataContext.ObtenerParametros("MAIL_PASSWORD")?.Valor ?? "";
            ssl = dataContext.ObtenerParametros("MAIL_SSL") != null
            && bool.Parse(dataContext.ObtenerParametros("MAIL_SSL").Valor);


            var smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Port = Port;
            smtp.EnableSsl = ssl;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(user, pass);


            return new DataMail(
                smtp,
                new MailAddress(MailAddress));
        }

        public static string GenerateBodyMailFromDepartamento(this DepartamentoModel departamento)
        {
            string body = "";
            if (departamento.Data.Count > 0)
            {

                body = "<div class='card'>" +
                                "<div id='heading11' class='card-header' data-toggle='collapse' role='button' data-target='#accordion-" + departamento.Id + "' aria-expanded='false' aria-controls='accordion10'>" +
                                    "<h3 class='lead collapse-title'> " + departamento.Titulo + "<b> (" + departamento.Total.ToString() + ")</b></h3>" +
                                "</div>" +
                              "<div id='accordion-" + departamento.Id.ToString() + "' role='tabpanel' data-parent='#accordionWrapa10' aria-labelledby='heading11' class='collapse'>";
                body += "<div class='card-body table-responsive'>" +
                          "<table>" +
                              "<thead style='background: #004C3B; color: #FFF;'>" +
                                  "<tr>" +
                                      "<th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Indicador</th>" +
                                      "<th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Tipo</th>" +
                                      "<th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Departamento</th>" +
                                      "<th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Codigo</th>" +
                                      "<th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Nombre</th>" +
                                      "<th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Posicion</th>" +
                                      "<th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Horario</th>" +
                                      "<th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Entrada</th>" +
                                     " <th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Salida</th>" +
                                    "  <th   style='border: 1px solid #DDD; padding: 5px; text-transform: uppercase;'>Ctd.Horas</th>" +
                               "   </tr>" +
                              "</thead>" +
                             " <tbody>";

                foreach (var item in departamento.Data)
                {
                    string semaforo = "";
                    if (item.Indicador.Indicador == "ausenciajust") { semaforo = " background: #808080;"; }
                    else if (item.Indicador.Indicador == "presentes") { semaforo = " background: #00ff21;"; }
                    else if (item.Indicador.Indicador == "inasistencias") { semaforo = " background: #ff0000;"; }
                    else if (item.Indicador.Indicador == "tardanzas") { semaforo = " background: #ffd800;"; }
                    else if (item.Indicador.Indicador == "offpremise") { semaforo = " background: #00A6D6;"; }
                    else if (item.Indicador.Indicador == "condriesgo") { semaforo = " background: #808080;"; }
                    else if (item.Indicador.Indicador == "cuarentena") { semaforo = " background: #808080;"; }
                    else if (item.Indicador.Indicador == "notocatrabajar") { semaforo = " background: #808080;"; }


                    body += "<tr>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px; " + semaforo + "'></td>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px;'>" + item.Indicador.Tipo + "</td>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px;'>" + item.Departamento + "</td>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px;'>" + item.Codigo + "</td>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px; text-transform: capitalize;'>" + item.Nombre + "</td>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px;'>" + item.Posicion + "</td>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px;'>" + item.Horario + "</td>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px;'>" + item.Entrada + "</td>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px;'>" + item.Salida + "</td>" +
                                        "<td nowrap   style='border: 1px solid #DDD; padding: 5px;'>" + item.CantHoras + "</td>" +
                                    "</tr>";
                }

                body += "</tbody>" +
                                        "</table> " +
                                   "</div> " +
                                "</div> " +
                            "</div>";
            }
            return body;
        }
    }
}
