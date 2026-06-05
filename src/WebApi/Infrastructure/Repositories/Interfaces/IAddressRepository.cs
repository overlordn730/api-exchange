using WebApi.Domain.Dto.Addresses;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Repositories.Interfaces;

public interface IAddressRepository
{
    Task<IEnumerable<AddressResponse>> GetByUserId(int userId);
    Task<Address> Create(Address address);
    Task<Address> Update(int id, Address address);
    Task Delete(int id);
}