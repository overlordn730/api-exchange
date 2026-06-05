using Riok.Mapperly.Abstractions;
using WebApi.Domain.Dto.Addresses;
using WebApi.Domain.Entities;

namespace WebApi.Application.Mapper;

[Mapper]
public partial class AddressMapper
{
    // Entidad → Response
    [MapperIgnoreSource(nameof(Address.User))]
    public partial AddressResponse MapToResponse(Address address);

    // Request → Entidad
    [MapperIgnoreTarget(nameof(Address.Id))]
    [MapperIgnoreTarget(nameof(Address.UserId))]
    [MapperIgnoreTarget(nameof(Address.User))]
    public partial Address MapToEntity(AddressRequest request);
}