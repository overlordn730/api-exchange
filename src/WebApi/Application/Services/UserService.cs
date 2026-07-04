using WebApi.Application.Exceptions;
using WebApi.Application.Mapper;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Users;
using WebApi.Infrastructure.Repositories.Interfaces;

namespace WebApi.Application.Services;

public class UserService(IUserRepository repository, UserMapper mapper, ILogger<UserService> logger) : IUserService
{
    private readonly IUserRepository _repository = repository;
    private readonly UserMapper _mapper = mapper;
    private readonly ILogger<UserService> _logger = logger;

    public async Task<IEnumerable<UserResponse>> GetAll(bool? isActive)
    {
        _logger.LogInformation("Obteniendo usuarios. Filtro isActive: {isActive}", isActive);
        return await _repository.GetAll(isActive);
    }

    public async Task<UserResponse> GetById(int id)
    {
        _logger.LogInformation("Obteniendo usuario {id}", id);
        return await _repository.GetById(id);
    }

    public async Task<UserResponse> Create(UserRequest request)
    {
        _logger.LogInformation("Creando usuario {@request}", request);

        if (await _repository.EmailExists(request.Email))
            throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.EMAIL_ALREADY_EXISTS);

        var user = _mapper.MapToEntity(request);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        user.IsActive = true;

        var created = await _repository.Create(user);
        return _mapper.MapToResponse(created);
    }

    public async Task<UserResponse> Update(int id, UserRequest request)
    {
        _logger.LogInformation("Actualizando usuario {id}", id);

        var existing = await _repository.GetById(id);

        if (existing.Email != request.Email && await _repository.EmailExists(request.Email))
            throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.EMAIL_ALREADY_EXISTS);

        var user = _mapper.MapToEntity(request);
        user.IsActive = request.IsActive;

        // Solo actualizar password si se envió una nueva
        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var updated = await _repository.Update(id, user);
        return _mapper.MapToResponse(updated);
    }

    public async Task Delete(int id)
    {
        _logger.LogInformation("Eliminando usuario {id}", id);
        await _repository.Delete(id);
    }
}