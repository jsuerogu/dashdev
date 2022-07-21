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
    public class RepositoryParametroCorreos : IRepositoryParametroCorreos
    {

        private readonly insitedb context;

        public RepositoryParametroCorreos(insitedb context)
        {
            
            this.context = context;
        }

        public Task<int> AddAsync(ParametroCorreos entity, CancellationToken cancellationToken = default)
        {
            context.ParametroCorreos.AddAsync(entity, cancellationToken);
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(ParametroCorreos entity, CancellationToken cancellationToken = default)
        {
            context.Remove(entity);
            return context.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> ExistAsync(Expression<Func<ParametroCorreos, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return context.ParametroCorreos.AnyAsync(predicate, cancellationToken);
        }

        public Task<ParametroCorreos> FindAsync(int pk, CancellationToken cancellationToken = default)
        {
            return context.ParametroCorreos.FirstOrDefaultAsync(e => e.Id == pk);
        }

        public Task<IQueryable<ParametroCorreos>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(context.ParametroCorreos.AsQueryable());
        }

        public Task<int> UpdateAsync(ParametroCorreos entity, CancellationToken cancellationToken = default)
        {
            context.Update(entity);
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
