using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Addresses;

namespace WebApi.Application.Addresses.Commands.Handlers;

public class CreateAddressHandler(
    IAddressService service,
    ILogger<CreateAddressHandler> logger
) : IRequestHandler<CreateAddressRequest, AddressResponse>
{
    private readonly IAddressService _service = service;
    private readonly ILogger<CreateAddressHandler> _logger = logger;

    public async Task<AddressResponse> Handle(
        CreateAddressRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creando dirección para usuario {userId}", request.UserId);

        return await _service.Create(request.UserId, new AddressRequest
        {
            Street = request.Street,
            City = request.City,
            Country = request.Country,
            ZipCode = request.ZipCode
        });
    }
}