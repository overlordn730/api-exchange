using Riok.Mapperly.Abstractions;
using WebApi.Domain.Dto.Users;
using WebApi.Domain.Entities;

namespace WebApi.Application.Mapper;

[Mapper]
public partial class UserMapper
{
    // Entidad → Response
    [MapperIgnoreSource(nameof(User.PasswordHash))]
    [MapperIgnoreSource(nameof(User.Addresses))]
    public partial UserResponse MapToResponse(User user);

    // Request → Entidad
    [MapperIgnoreSource(nameof(UserRequest.Password))]
    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(User.PasswordHash))]
    [MapperIgnoreTarget(nameof(User.IsActive))]
    [MapperIgnoreTarget(nameof(User.Addresses))]
    public partial User MapToEntity(UserRequest request);
}