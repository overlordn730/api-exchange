using Microsoft.EntityFrameworkCore;
using WebApi.Application.Exceptions;
using WebApi.Domain.Dto.Users;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Data;
using WebApi.Infrastructure.Repositories.Interfaces;

namespace WebApi.Infrastructure.Repositories;

public class UserRepository(OracleDbContext context) : IUserRepository
{
    private readonly OracleDbContext _context = context;

    public async Task<IEnumerable<UserResponse>> GetAll(bool? isActive)
    {
        var query = _context.Users.AsQueryable();

        if (isActive.HasValue)
            query = query.Where(u => u.IsActive == isActive.Value);

        return await query
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                IsActive = u.IsActive
            })
            .ToListAsync();
    }

    public async Task<UserResponse> GetById(int id)
    {
        return await _context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                IsActive = u.IsActive
            })
            .FirstOrDefaultAsync()
            ?? throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.USER_NOT_FOUND);
    }

    public async Task<User> Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> Update(int id, User user)
    {
        var existing = await _context.Users.FindAsync(id)
            ?? throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.USER_NOT_FOUND);

        existing.Name = user.Name;
        existing.Email = user.Email;
        existing.IsActive = user.IsActive;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task Delete(int id)
    {
        var existing = await _context.Users.FindAsync(id)
            ?? throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.USER_NOT_FOUND);

        _context.Users.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailExists(string email)
    {
        var count = await _context.Users
            .Where(u => u.Email == email)
            .CountAsync();

        return count > 0;
    }
}