using MediatR;

namespace WebApi.Application.Users.Commands;

public class DeleteUserRequest : IRequest
{
    public int Id { get; set; }
}