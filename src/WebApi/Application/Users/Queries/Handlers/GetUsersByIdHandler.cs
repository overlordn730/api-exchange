using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Users;

namespace WebApi.Application.Users.Queries.Handlers;

public class GetUserByIdHandler(
    IUserService service,
    ILogger<GetUserByIdHandler> logger
) : IRequestHandler<GetUserByIdRequest, UserResponse>
{
    private readonly IUserService _service = service;
    private readonly ILogger<GetUserByIdHandler> _logger = logger;

    public async Task<UserResponse> Handle(
        GetUserByIdRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obteniendo usuario {id}", request.Id);
        return await _service.GetById(request.Id);
    }
}