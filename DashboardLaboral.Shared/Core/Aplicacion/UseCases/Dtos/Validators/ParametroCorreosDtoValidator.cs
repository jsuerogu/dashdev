using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using DashboarLaboral.Extensions;
using FluentValidation;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos.Validators
{
    public class ParametroCorreosDtoValidator : AbstractValidator<ParametroCorreosDto>
    {
        private readonly IRepositoryParametroCorreos repository;

        public ParametroCorreosDtoValidator(IRepositoryParametroCorreos repository)
        {
            this.CascadeMode = CascadeMode.Stop;
            try
            {

                RuleFor(p => p.Destinatario)
                    .NotEmpty()
                    .WithMessage("Destinatario es requerido!")
                    .Must(a => string.IsNullOrEmpty(a) || !a.Split(',', StringSplitOptions.RemoveEmptyEntries).Any(s => !s.IsCorrectEmail()))
                    .WithMessage("No todos los destinatarios tienen un formato de correo correcto!");


                RuleFor(p => p.Empresa)
                    .NotEmpty()
                    .WithMessage("Empresa es requerida");


                RuleFor(p => p.Vicepresidencia)
                    .NotEmpty()
                    .WithMessage("Vicepresidencia es requerida");

                RuleFor(p => p.Departamento)
                    .NotEmpty()
                    .WithMessage("Departamento es requerido");


                RuleFor(p => p.Indicadores.Where(i => i.Selected))
                    .NotEmpty()
                    .WithMessage("Debe seleccionar al menos un indicador"); ;

                RuleFor(p => p.Empresa)
                    .MustAsync((a, _, c) => ExisteValor(e => e.Empresa == _ && e.Vicepresidencia == a.Vicepresidencia && e.Departamento == a.Departamento && e.Id != a.Id, c))
                    .WithMessage((e, v) => $"Parámetro con valores {e.Empresa}/{e.Vicepresidencia}/{e.Departamento} ya existe!");

                this.repository = repository;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> ExisteValor(Expression<Func<ParametroCorreos, bool>> predicate, CancellationToken cancellationToken)
        {

            return !(await repository.ExistAsync(predicate, cancellationToken));
        }
    }
}
