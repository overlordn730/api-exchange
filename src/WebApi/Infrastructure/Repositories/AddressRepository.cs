using Microsoft.EntityFrameworkCore;
using WebApi.Application.Exceptions;
using WebApi.Domain.Dto.Addresses;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Data;
using WebApi.Infrastructure.Repositories.Interfaces;

namespace WebApi.Infrastructure.Repositories;

public class AddressRepository(OracleDbContext context) : IAddressRepository
{
    private readonly OracleDbContext _context = context;

    public async Task<IEnumerable<AddressResponse>> GetByUserId(int userId)
    {
        var userExists = await _context.Users
            .Where(u => u.Id == userId)
            .CountAsync() > 0;

        if (!userExists)
            throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.USER_NOT_FOUND);

        return await _context.Addresses
            .Where(a => a.UserId == userId)
            .Select(a => new AddressResponse
            {
                Id = a.Id,
                UserId = a.UserId,
                Street = a.Street,
                City = a.City,
                Country = a.Country,
                ZipCode = a.ZipCode
            })
            .ToListAsync();
    }

    public async Task<Address> Create(Address address)
    {
        var userExists = await _context.Users.Where(u => u.Id == address.UserId).CountAsync() > 0;

        if (!userExists)
            throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.USER_NOT_FOUND);

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<Address> Update(int id, Address address)
    {
        var existing = await _context.Addresses.FindAsync(id)
            ?? throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.ADDRESS_NOT_FOUND);

        existing.Street = address.Street;
        existing.City = address.City;
        existing.Country = address.Country;
        existing.ZipCode = address.ZipCode;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task Delete(int id)
    {
        var existing = await _context.Addresses.FindAsync(id)
            ?? throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.ADDRESS_NOT_FOUND);

        _context.Addresses.Remove(existing);
        await _context.SaveChangesAsync();
    }
}