using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using FluentValidation;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos.Validators
{
    public class AusentismoDtoValidator : AbstractValidator<AusentismoDto>
    {
        private readonly IRepositoryAusentismo repository;

        public AusentismoDtoValidator(IRepositoryAusentismo repository)
        {
            CascadeMode = CascadeMode.Stop;
            try
            {
                RuleFor(p => p.Aucod)
                    .NotEmpty()
                    .WithMessage("Código de ausentismo es requerido")
                    .MaximumLength(10)
                    .WithMessage("El máximo es de 10 caracteres")
                    .MustAsync((a, b, c) => ExisteValor(e => e.Aucod.ToLower().Trim() == a.Aucod.ToLower().Trim() && e.Id != a.Id, c))
                    .WithMessage((e, v) => $"Código {v} ya esta registrado!");

                RuleFor(p => p.Audes)
                    .NotEmpty()
                    .WithMessage("Descrpción de ausentismo es requerido")
                    .MaximumLength(20)
                    .WithMessage("El máximo es de 20 caracteres")
                    .MustAsync((a, b, c) => ExisteValor(e => e.Audes.ToLower().Trim() == a.Audes.ToLower().Trim() && e.Id != a.Id, c))
                    .WithMessage((e, v) => $"Nombre {v} ya esta registrado!");


                this.repository = repository;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> ExisteValor(Expression<Func<Ausentismo, bool>> predicate, CancellationToken cancellationToken)
        {
            
            return !(await repository.ExistAsync(predicate, cancellationToken));
        }
    }
}
