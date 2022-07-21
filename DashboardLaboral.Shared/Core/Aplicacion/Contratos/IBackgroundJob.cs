using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.Contratos
{
    public interface IBackgroundJob
    {
        void Prepare();
        Task ExecuteAsync(CancellationToken cancellationToken = new());
    }
}
