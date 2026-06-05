using FluentValidation;
using WebApi.Domain.Dto.Addresses;

namespace WebApi.Application.Validators;

public class AddressValidator : AbstractValidator<AddressRequest>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("La calle es requerida")
            .MaximumLength(200).WithMessage("La calle no puede superar los 200 caracteres");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es requerida")
            .MaximumLength(100).WithMessage("La ciudad no puede superar los 100 caracteres");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("El país es requerido")
            .MaximumLength(100).WithMessage("El país no puede superar los 100 caracteres");

        RuleFor(x => x.ZipCode)
            .MaximumLength(20).WithMessage("El código postal no puede superar los 20 caracteres")
            .When(x => x.ZipCode != null);
    }
}