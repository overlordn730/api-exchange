using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Users;

namespace WebApi.Application.Users.Queries.Handlers;

public class GetUsersHandler(
    IUserService service,
    ILogger<GetUsersHandler> logger
) : IRequestHandler<GetUsersRequest, IEnumerable<UserResponse>>
{
    private readonly IUserService _service = service;
    private readonly ILogger<GetUsersHandler> _logger = logger;

    public async Task<IEnumerable<UserResponse>> Handle(
        GetUsersRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obteniendo usuarios");
        return await _service.GetAll(request.IsActive);
    }
}