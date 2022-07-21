using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.Contratos.Base
{
    public interface IRepositoryAsync<T, TPK>
        where T : class
    {
        Task<int> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> FindAsync(TPK pk, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
