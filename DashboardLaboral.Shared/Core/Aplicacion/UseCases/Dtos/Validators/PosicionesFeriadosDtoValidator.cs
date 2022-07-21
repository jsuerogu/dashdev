using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using FluentValidation;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos.Validators
{
    public class PosicionesFeriadosDtoValidator : AbstractValidator<PosicionesFeriadosDto>
    {
        private readonly IRepositoryPosicionesFeriados repository;
        public PosicionesFeriadosDtoValidator(IRepositoryPosicionesFeriados repository)
        {
            RuleFor(p => p.Posicion)
                .NotEmpty()
                .WithMessage("Posicion es requerida")
                .MaximumLength(400)
                .WithMessage("El máximo es de 400 caracteres");

            RuleFor(p => p.Posicion)
                    .MustAsync((a, _, c) => ExisteValor(e => e.Posicion == a.Posicion && e.Id != a.Id, c))
                    .WithMessage((e, v) => $"Posición {e.Posicion} ya existe!");

            this.repository = repository;
        }
        private async Task<bool> ExisteValor(Expression<Func<PosicionesFeriados, bool>> predicate, CancellationToken cancellationToken)
        {

            return !(await repository.ExistAsync(predicate, cancellationToken));
        }
    }
}
