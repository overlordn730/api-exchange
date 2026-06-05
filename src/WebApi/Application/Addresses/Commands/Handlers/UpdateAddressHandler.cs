using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Addresses;

namespace WebApi.Application.Addresses.Commands.Handlers;

public class UpdateAddressHandler(
    IAddressService service,
    ILogger<UpdateAddressHandler> logger
) : IRequestHandler<UpdateAddressRequest, AddressResponse>
{
    private readonly IAddressService _service = service;
    private readonly ILogger<UpdateAddressHandler> _logger = logger;

    public async Task<AddressResponse> Handle(
        UpdateAddressRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Actualizando dirección {id}", request.Id);

        return await _service.Update(request.Id, new AddressRequest
        {
            Street = request.Street,
            City = request.City,
            Country = request.Country,
            ZipCode = request.ZipCode
        });
    }
}