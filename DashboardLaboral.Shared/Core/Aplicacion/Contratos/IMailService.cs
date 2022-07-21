using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Models;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.Contratos
{
    public interface IMailService
    {
        Task SendMailAsync(MailTemplate template, IDataContext dataContext);
    }
}
