using MediatR;
using WebApi.Application.Services.Interfaces;

namespace WebApi.Application.Addresses.Commands.Handlers;

public class DeleteAddressHandler(
    IAddressService service,
    ILogger<DeleteAddressHandler> logger
) : IRequestHandler<DeleteAddressRequest>
{
    private readonly IAddressService _service = service;
    private readonly ILogger<DeleteAddressHandler> _logger = logger;

    public async Task Handle(
        DeleteAddressRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Eliminando dirección {id}", request.Id);
        await _service.Delete(request.Id);
    }
}