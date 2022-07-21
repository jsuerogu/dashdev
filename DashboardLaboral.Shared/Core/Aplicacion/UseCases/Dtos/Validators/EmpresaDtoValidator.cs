using DashboarLaboral.Core.Aplicacion.Contratos;
using DashboarLaboral.Data;
using FluentValidation;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos.Validators
{
    public class EmpresaDtoValidator : AbstractValidator<EmpresaDto>
    {
        private readonly IRepositoryEmpresa repository;

        public EmpresaDtoValidator(IRepositoryEmpresa repository)
        {
            this.CascadeMode = CascadeMode.Stop;
            try
            {
                RuleFor(p => p.Codigo)
                    .NotEmpty()
                    .WithMessage("Código de empresa es requerido")
                    .MaximumLength(10)
                    .WithMessage("El máximo es de 10 caracteres")
                    .MustAsync((a, b, c) => ExisteValor(e => e.CodigoEmpresa.ToLower().Trim() == a.Codigo.ToLower().Trim() && e.RowId != a.RowId, c))
                    .WithMessage((e, v) => $"Código {v} ya esta registrado!");

                RuleFor(p => p.Nombre)
                    .NotEmpty()
                    .WithMessage("Nombre de empresa es requerido")
                    .MaximumLength(20)
                    .WithMessage("El máximo es de 20 caracteres")
                    .MustAsync((a, b, c) => ExisteValor(e => e.Empresa1.ToLower().Trim() == a.Nombre.ToLower().Trim() && e.RowId != a.RowId, c))
                    .WithMessage((e, v) => $"Nombre {v} ya esta registrado!");

                RuleFor(p => p.Color)
                    .MaximumLength(10)
                    .WithMessage("El máximo es de 10 caracteres");

                this.repository = repository;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> ExisteValor(Expression<Func<Empresa, bool>> predicate, CancellationToken cancellationToken)
        {
            
            return !(await repository.ExistAsync(predicate, cancellationToken));
        }
    }
}
