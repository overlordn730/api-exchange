using AspNetCore.Authentication.ApiKey;

namespace WebApi.Extensions;

public static class AuthenticationServiceExtensions
{
    public static void AddApiKeyAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
            .AddApiKeyInHeader<ApiKeyProvider>(options =>
            {
                options.Realm = configuration["ApiKeyConfiguration:Realm"] ?? "api-exchange";
                options.KeyName = configuration["ApiKeyConfiguration:Header"] ?? "X-API-KEY";
            });

        services.AddSingleton<IApiKeyProvider, ApiKeyProvider>();
    }
}