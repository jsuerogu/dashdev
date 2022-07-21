using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using FluentValidation;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos.Validators
{
    public class PosicionOffPremiseDtoValidator : AbstractValidator<PosicionOffPremiseHeaderDto>
    {
        private readonly IRepositoryPosicionOffPremise repository;

        public PosicionOffPremiseDtoValidator(IRepositoryPosicionOffPremise repository)
        {
            CascadeMode = CascadeMode.Stop;
            try
            {
                RuleFor(p => p.Empresa)
                    .NotEmpty()
                    .WithMessage("Empresa es requerida");

                RuleFor(p => p.VicePresidencia)
                    .NotEmpty()
                    .WithMessage("Vicepresidencia es requerida");

                RuleFor(p => p.Departamento)
                    .NotEmpty()
                    .WithMessage("Departamento es requerido");

                RuleFor(p => p.Posicion)
                    .NotEmpty()
                    .WithMessage("Posición es requerido");



                RuleFor(p => p.Empresa)
                    .MustAsync((a, _, c) => ExisteValor(e => e.Empresa == _ && e.VicePresidencia == a.VicePresidencia && e.Departamento == a.Departamento && e.Posicion == a.Posicion && e.Id != a.Id, c))
                    .WithMessage((e, v) => $"Posición {e.Posicion} ya existe!");

                this.repository = repository;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> ExisteValor(Expression<Func<PosicionOffPremiseHeader, bool>> predicate, CancellationToken cancellationToken)
        {

            return !(await repository.ExistAsync(predicate, cancellationToken));
        }
    }
}
