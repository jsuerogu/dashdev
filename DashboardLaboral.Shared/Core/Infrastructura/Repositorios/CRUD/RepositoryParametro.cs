using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DashboarLaboral.Core.Infrastructura.Repositorios.CRUD
{
    public class RepositoryParametro : IRepositoryParametro
    {
        private readonly insitedb context;

        public RepositoryParametro(insitedb context)
        {
            this.context = context;
        }

        public Task<int> AddAsync(Parametro entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Parametro entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(Expression<Func<Parametro, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return context.Parametros.AnyAsync(predicate, cancellationToken);
        }

        public Task<Parametro> FindAsync(Guid pk, CancellationToken cancellationToken = default)
        {
            return context.Parametros.FirstOrDefaultAsync(e => e.RowId == pk);
        }

        public Task<IQueryable<Parametro>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(context.Parametros.AsQueryable());
        }

        public Task<int> UpdateAsync(Parametro entity, CancellationToken cancellationToken = default)
        {
            context.Update(entity);
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
