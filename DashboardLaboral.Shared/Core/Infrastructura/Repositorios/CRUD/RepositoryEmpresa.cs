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
    public class RepositoryEmpresa : IRepositoryEmpresa
    {
        private readonly insitedb context;

        public RepositoryEmpresa(insitedb context)
        {
            this.context = context;
        }

        public Task<int> AddAsync(Empresa entity, CancellationToken cancellationToken = default)
        {
            context.Empresas.AddAsync(entity, cancellationToken);
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(Empresa entity, CancellationToken cancellationToken = default)
        {
            context.Remove(entity);
            return context.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> ExistAsync(Expression<Func<Empresa, bool>> predicate, CancellationToken cancellationToken = default)
        {
            try
            {
                return context.Empresas.AnyAsync(predicate, cancellationToken);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<Empresa> FindAsync(string pk, CancellationToken cancellationToken = default)
        {
            return context.Empresas.FirstOrDefaultAsync(e => e.CodigoEmpresa.ToLower().Trim() == pk.ToLower().Trim());
        }

        public Task<IQueryable<Empresa>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(context.Empresas.AsQueryable());
        }

        public Task<int> UpdateAsync(Empresa entity, CancellationToken cancellationToken = default)
        {
            context.Update(entity);
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
