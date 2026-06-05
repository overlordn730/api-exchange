using AspNetCore.Authentication.ApiKey;
using System.Security.Claims;

namespace WebApi.Extensions;

public class ApiKeyProvider(
    ILogger<ApiKeyProvider> logger,
    IConfiguration configuration
) : IApiKeyProvider
{
    private readonly ILogger<ApiKeyProvider> _logger = logger;
    private readonly string _apiKey = configuration.GetValue<string>("ApiKeyConfiguration:Key")
        ?? throw new ArgumentException("No se encuentra el ApiKey en la configuración.");

    public Task<IApiKey?> ProvideAsync(string key)
    {
        if (key.Equals(_apiKey))
        {
            _logger.LogDebug("ApiKey válido");
            return Task.FromResult<IApiKey?>(new ApiKeyResult(key));
        }

        _logger.LogWarning("ApiKey inválido");
        return Task.FromResult<IApiKey?>(null);
    }
}

public class ApiKeyResult : IApiKey
{
    public string Key { get; }
    public string OwnerName { get; } = "Api.Exchange";
    public IReadOnlyCollection<Claim> Claims { get; } = new List<Claim>();
    public ApiKeyResult(string key) => Key = key;
}