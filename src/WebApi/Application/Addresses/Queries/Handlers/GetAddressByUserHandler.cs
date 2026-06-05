using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Addresses;

namespace WebApi.Application.Addresses.Queries.Handlers;

public class GetAddressesByUserHandler(
    IAddressService service,
    ILogger<GetAddressesByUserHandler> logger
) : IRequestHandler<GetAddressesByUserRequest, IEnumerable<AddressResponse>>
{
    private readonly IAddressService _service = service;
    private readonly ILogger<GetAddressesByUserHandler> _logger = logger;

    public async Task<IEnumerable<AddressResponse>> Handle(
        GetAddressesByUserRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obteniendo direcciones del usuario {userId}", request.UserId);
        return await _service.GetByUserId(request.UserId);
    }
}