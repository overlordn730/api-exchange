using WebApi.Domain.Dto.Users;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserResponse>> GetAll(bool? isActive);
    Task<UserResponse> GetById(int id);
    Task<User> Create(User user);
    Task<User> Update(int id, User user);
    Task Delete(int id);
    Task<bool> EmailExists(string email);
}