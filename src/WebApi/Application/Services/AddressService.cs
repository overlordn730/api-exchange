using WebApi.Application.Mapper;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Addresses;
using WebApi.Infrastructure.Repositories.Interfaces;

namespace WebApi.Application.Services;

public class AddressService(
    IAddressRepository repository,
    AddressMapper mapper,
    ILogger<AddressService> logger
) : IAddressService
{
    private readonly IAddressRepository _repository = repository;
    private readonly AddressMapper _mapper = mapper;
    private readonly ILogger<AddressService> _logger = logger;

    public async Task<IEnumerable<AddressResponse>> GetByUserId(int userId)
    {
        _logger.LogInformation("Obteniendo direcciones del usuario {userId}", userId);
        return await _repository.GetByUserId(userId);
    }

    public async Task<AddressResponse> Create(int userId, AddressRequest request)
    {
        _logger.LogInformation("Creando dirección para usuario {userId}", userId);

        var address = _mapper.MapToEntity(request);
        address.UserId = userId;

        var created = await _repository.Create(address);
        return _mapper.MapToResponse(created);
    }

    public async Task<AddressResponse> Update(int id, AddressRequest request)
    {
        _logger.LogInformation("Actualizando dirección {id}", id);

        var address = _mapper.MapToEntity(request);
        var updated = await _repository.Update(id, address);
        return _mapper.MapToResponse(updated);
    }

    public async Task Delete(int id)
    {
        _logger.LogInformation("Eliminando dirección {id}", id);
        await _repository.Delete(id);
    }
}