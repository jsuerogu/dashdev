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
    public class RepositoryPosicionesFeriados : IRepositoryPosicionesFeriados
    {
        private readonly insitedb context;

        public RepositoryPosicionesFeriados(insitedb context)
        {
            this.context = context;
        }

        public Task<int> AddAsync(PosicionesFeriados entity, CancellationToken cancellationToken = default)
        {
            context.PosicionesFeriados.AddAsync(entity, cancellationToken);
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(PosicionesFeriados entity, CancellationToken cancellationToken = default)
        {
            context.Remove(entity);
            return context.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> ExistAsync(Expression<Func<PosicionesFeriados, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return context.PosicionesFeriados.AnyAsync(predicate, cancellationToken);
        }

        public Task<PosicionesFeriados> FindAsync(int pk, CancellationToken cancellationToken = default)
        {
            return context.PosicionesFeriados.FirstOrDefaultAsync(e => e.Id == pk);
        }

        public Task<IQueryable<PosicionesFeriados>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(context.PosicionesFeriados.AsQueryable());
        }

        public Task<int> UpdateAsync(PosicionesFeriados entity, CancellationToken cancellationToken = default)
        {
            context.Update(entity);
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
