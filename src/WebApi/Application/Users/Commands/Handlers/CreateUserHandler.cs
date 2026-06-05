using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Users;

namespace WebApi.Application.Users.Commands.Handlers;

public class CreateUserHandler(
    IUserService service,
    ILogger<CreateUserHandler> logger
) : IRequestHandler<CreateUserRequest, UserResponse>
{
    private readonly IUserService _service = service;
    private readonly ILogger<CreateUserHandler> _logger = logger;

    public async Task<UserResponse> Handle(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando creación de usuario {@request}", request);

        return await _service.Create(new UserRequest
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        });
    }
}