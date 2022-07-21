using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Repositorios.CRUD
{
    public class RepositoryPosicionOffPremise : IRepositoryPosicionOffPremise
    {
        private readonly insitedb context;
        private readonly IDataContext dataContext;

        public RepositoryPosicionOffPremise(insitedb context, IDataContext dataContext)
        {
            this.context = context;
            this.dataContext = dataContext;
        }

        public Task<int> AddAsync(PosicionOffPremiseHeader entity, CancellationToken cancellationToken = default)
        {
            entity.Details = new Collection<PosicionOffPremiseDetails>(dataContext.ObtenerListaEmpleados(entity.Empresa, entity.VicePresidencia, entity.Departamento, entity.Posicion)
                .Select(e => new PosicionOffPremiseDetails { CodigoEmpleado = int.Parse(e.Value), Selected = true }).ToList());
            context.PosicionOffpremiseHeader.AddAsync(entity, cancellationToken);

            return context.SaveChangesAsync();
        }

        public Task<int> AddDetailsAsync(PosicionOffPremiseDetails entity, CancellationToken cancellationToken = default)
        {
            context.PosicionOffpremiseDetails.AddAsync(entity, cancellationToken);
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(PosicionOffPremiseHeader entity, CancellationToken cancellationToken = default)
        {
            context.Remove(entity);
            return context.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> ExistAsync(Expression<Func<PosicionOffPremiseHeader, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return context.PosicionOffpremiseHeader.AnyAsync(predicate, cancellationToken);
        }

        public Task<PosicionOffPremiseHeader> FindAsync(int pk, CancellationToken cancellationToken = default)
        {
            return context.PosicionOffpremiseHeader.FirstOrDefaultAsync(e => e.Id == pk);
        }

        public Task<IQueryable<PosicionOffPremiseHeader>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(context.PosicionOffpremiseHeader.AsQueryable());
        }

        public Task<PosicionOffPremiseHeader> GetWithIncludeAsync(int id)
        {
            return context.PosicionOffpremiseHeader.Include(s => s.Details).FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<int> UpdateAsync(PosicionOffPremiseHeader entity, CancellationToken cancellationToken = default)
        {
            context.Update(entity);
            return context.SaveChangesAsync(cancellationToken);
        }

        public Task<int> UpdateDetailsAsync(PosicionOffPremiseDetails entity, CancellationToken cancellationToken = default)
        {
            context.Update(entity);
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
