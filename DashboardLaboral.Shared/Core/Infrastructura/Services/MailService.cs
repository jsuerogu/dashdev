using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Extensions;
using DashboarLaboral.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Services
{
    public class MailService : IMailService
    {
        public async Task SendMailAsync(MailTemplate template, IDataContext dataContext)
        {
            List<Task> tasks = new List<Task>();
            template.MailMessages.ForEach(mail =>
            {
                var dataMail = dataContext.CreateSmtpClient();
                mail.From = dataMail.FromAddress;
                tasks.Add(dataMail.Smtp.SendMailAsync(mail));
            });

            await Task.WhenAll(tasks);
        }
    }
}
