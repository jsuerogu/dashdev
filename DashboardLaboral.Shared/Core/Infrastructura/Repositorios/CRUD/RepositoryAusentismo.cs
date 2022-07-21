using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Repositorios.CRUD
{
    public class RepositoryAusentismo : IRepositoryAusentismo
    {
        private readonly insitedb context;

        public RepositoryAusentismo(insitedb context)
        {
            this.context = context;
        }

        public Task<int> AddAsync(Ausentismo entity, CancellationToken cancellationToken = default)
        {
            entity.Id = context.Ausentismos.Max(p => Convert.ToInt32(p.Id) + 1).ToString();

            context.Ausentismos.AddAsync(entity, cancellationToken);
            var result = context.SaveChangesAsync();
            return result;

        }

        public Task<int> DeleteAsync(Ausentismo entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(Expression<Func<Ausentismo, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return context.Ausentismos.AnyAsync(predicate, cancellationToken);
        }

        public Task<Ausentismo> FindAsync(string pk, CancellationToken cancellationToken = default)
        {
            return context.Ausentismos.FirstOrDefaultAsync(e => e.Id == pk);
        }

        public Task<IQueryable<Ausentismo>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(context.Ausentismos.AsQueryable());
        }

        public Task<int> UpdateAsync(Ausentismo entity, CancellationToken cancellationToken = default)
        {
            context.Update(entity);
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
