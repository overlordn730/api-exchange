using MediatR;
using WebApi.Application.Services.Interfaces;

namespace WebApi.Application.Users.Commands.Handlers;

public class DeleteUserHandler(
    IUserService service,
    ILogger<DeleteUserHandler> logger
) : IRequestHandler<DeleteUserRequest>
{
    private readonly IUserService _service = service;
    private readonly ILogger<DeleteUserHandler> _logger = logger;

    public async Task Handle(
        DeleteUserRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando eliminación de usuario {id}", request.Id);
        await _service.Delete(request.Id);
    }
}