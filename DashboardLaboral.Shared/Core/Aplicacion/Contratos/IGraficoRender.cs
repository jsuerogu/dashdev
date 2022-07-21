using DashboardLaboral.Shared.Models;
using DashboarLaboral.Models;
using System.Threading.Tasks;

namespace DashboardLaboral.Shared.Core.Aplicacion.Contratos
{
    public interface IGraficoRender
    {
        Task<RenderResponse> GetBase64(FiltroModel filtro);
    }
}
