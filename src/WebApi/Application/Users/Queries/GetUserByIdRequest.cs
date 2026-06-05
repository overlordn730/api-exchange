using MediatR;
using WebApi.Domain.Dto.Users;

namespace WebApi.Application.Users.Queries;

public class GetUserByIdRequest : IRequest<UserResponse>
{
    public int Id { get; set; }
}