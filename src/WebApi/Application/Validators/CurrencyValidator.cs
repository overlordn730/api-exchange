using FluentValidation;
using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Validators;

public class CurrencyValidator : AbstractValidator<CurrencyRequest>
{
    public CurrencyValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código es requerido")
            .MaximumLength(10).WithMessage("El código no puede superar los 3 caracteres - Ej: PYG");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres");

        RuleFor(x => x.RateToBase)
            .GreaterThan(0).WithMessage("La cotización debe ser mayor a 0");
    }
}