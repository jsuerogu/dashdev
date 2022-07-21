using FluentValidation;

namespace DashboarLaboral.Core.Aplicacion.UseCases.Dtos.Validators
{
    public class ParametroDtoValidator : AbstractValidator<ParametroDto>
    {
        public ParametroDtoValidator()
        {
            RuleFor(p => p.Valor)
                .NotEmpty()
                .WithMessage("Valor del parámetro es requerido")
                .MaximumLength(1000)
                .WithMessage("El máximo es de 1000 caracteres");

            RuleFor(p => p.Descripcion)
                .MaximumLength(500)
                .WithMessage("El máximo es de 500 caracteres");

            RuleFor(p => p.Mensaje)
                .MaximumLength(250)
                .WithMessage("El máximo es de 250 caracteres");

        }

    }
}
