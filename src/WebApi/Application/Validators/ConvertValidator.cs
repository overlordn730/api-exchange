using FluentValidation;
using WebApi.Application.Exceptions;
using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Validators;

public class ConvertValidator : AbstractValidator<ConvertRequest>
{
    public ConvertValidator()
    {
        RuleFor(x => x.FromCurrencyCode)
            .NotEmpty().WithMessage("La moneda de origen es requerida");

        RuleFor(x => x.ToCurrencyCode)
            .NotEmpty().WithMessage("La moneda de destino es requerida");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a 0");

        RuleFor(x => x)
            .Must(x => x.FromCurrencyCode != x.ToCurrencyCode)
            .WithMessage(Errors.CURRENCIES_SAME)
            .When(x => !string.IsNullOrEmpty(x.FromCurrencyCode) &&
                       !string.IsNullOrEmpty(x.ToCurrencyCode));
    }
}