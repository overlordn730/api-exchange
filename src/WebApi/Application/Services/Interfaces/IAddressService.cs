using WebApi.Domain.Dto.Addresses;

namespace WebApi.Application.Services.Interfaces;

public interface IAddressService
{
    Task<IEnumerable<AddressResponse>> GetByUserId(int userId);
    Task<AddressResponse> Create(int userId, AddressRequest request);
    Task<AddressResponse> Update(int id, AddressRequest request);
    Task Delete(int id);
}