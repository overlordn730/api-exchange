using Riok.Mapperly.Abstractions;
using WebApi.Domain.Dto.Currencies;
using WebApi.Domain.Entities;

namespace WebApi.Application.Mapper;

[Mapper]
public partial class CurrencyMapper
{
    // Entidad → Response
    public partial CurrencyResponse MapToResponse(Currency currency);

    // Request → Entidad
    [MapperIgnoreTarget(nameof(Currency.Id))]
    public partial Currency MapToEntity(CurrencyRequest request);
}