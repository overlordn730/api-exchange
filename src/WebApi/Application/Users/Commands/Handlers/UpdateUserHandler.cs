using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Users;

namespace WebApi.Application.Users.Commands.Handlers;

public class UpdateUserHandler(
    IUserService service,
    ILogger<UpdateUserHandler> logger
) : IRequestHandler<UpdateUserRequest, UserResponse>
{
    private readonly IUserService _service = service;
    private readonly ILogger<UpdateUserHandler> _logger = logger;

    public async Task<UserResponse> Handle(
        UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando actualización de usuario {id}", request.Id);

        return await _service.Update(request.Id, new UserRequest
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            IsActive = request.IsActive
        });
    }
}