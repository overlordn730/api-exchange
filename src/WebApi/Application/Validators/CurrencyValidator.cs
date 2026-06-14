using FluentValidation;
using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Validators;

public class CurrencyValidator : AbstractValidator<CurrencyRequest>
{
    public CurrencyValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código es requerido")
            .MaximumLength(10).WithMessage("El código no puede superar los 10 caracteres");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres");

        RuleFor(x => x.CountryCode)
            .NotEmpty().WithMessage("El código de país es requerido")
            .Length(2).WithMessage("El código de país debe tener 2 caracteres");

        RuleFor(x => x.BuyRate)
            .GreaterThan(0).WithMessage("La cotización de compra debe ser mayor a 0");

        RuleFor(x => x.SellRate)
            .GreaterThan(0).WithMessage("La cotización de venta debe ser mayor a 0");

        RuleFor(x => x)
            .Must(x => x.SellRate >= x.BuyRate)
            .WithMessage("La cotización de venta debe ser mayor o igual a la de compra")
            .When(x => x.BuyRate > 0 && x.SellRate > 0);
    }
}