using WebApi.Domain.Dto.Users;

namespace WebApi.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAll(bool? isActive);
    Task<UserResponse> GetById(int id);
    Task<UserResponse> Create(UserRequest request);
    Task<UserResponse> Update(int id, UserRequest request);
    Task Delete(int id);
}