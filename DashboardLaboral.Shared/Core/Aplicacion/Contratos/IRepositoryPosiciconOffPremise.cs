using DashboarLaboral.Core.Aplicacion.Contratos.Base;
using DashboarLaboral.Data;
using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.Contratos
{
    public interface IRepositoryPosicionOffPremise : IRepositoryAsync<PosicionOffPremiseHeader, int>
    {
        Task<PosicionOffPremiseHeader> GetWithIncludeAsync(int id);
        Task<int> AddDetailsAsync(PosicionOffPremiseDetails entity, CancellationToken cancellationToken = default);
        Task<int> UpdateDetailsAsync(PosicionOffPremiseDetails entity, CancellationToken cancellationToken = default);

    }
}
