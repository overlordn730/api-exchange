using MediatR;
using WebApi.Domain.Dto.Users;

namespace WebApi.Application.Users.Queries;

public class GetUsersRequest : IRequest<IEnumerable<UserResponse>>
{
    public bool? IsActive { get; set; }
}