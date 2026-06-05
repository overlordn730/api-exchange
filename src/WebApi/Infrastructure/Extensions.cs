using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.Mapper;
using WebApi.Application.Services;
using WebApi.Application.Services.Interfaces;
using WebApi.Infrastructure.Data;
using WebApi.Infrastructure.Repositories;
using WebApi.Infrastructure.Repositories.Interfaces;

namespace WebApi.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── Repositorios ──
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAddressRepository, AddressRepository>();
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();

        // ── Servicios ──
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAddressService, AddressService>();
        services.AddTransient<ICurrencyService, CurrencyService>();

        // ── Mappers ──
        services.AddSingleton(new UserMapper());
        services.AddSingleton(new AddressMapper());
        services.AddSingleton(new CurrencyMapper());

        // ── Middleware ──
        services.AddTransient<Application.Exceptions.ExceptionHandlingMiddleware>();

        // ── Validators ──
        services.AddValidatorsFromAssemblyContaining<Program>();

        // ── DbContext Oracle ──
        services.AddDbContext<OracleDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString("OracleDbContext")));

        return services;
    }
}