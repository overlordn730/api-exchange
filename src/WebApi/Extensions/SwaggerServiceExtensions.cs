using NSwag;
using NSwag.Generation.Processors.Security;

namespace WebApi.Extensions;

public static class SwaggerServiceExtensions
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddOpenApiDocument(options =>
        {
            options.PostProcess = document =>
            {
                document.Info = new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api.Exchange",
                    Description = "API de gestión de usuarios, direcciones y conversión de divisas.",
                    Contact = new OpenApiContact
                    {
                        Name = "Api.Exchange",
                        Email = "contacto@omega.com"
                    }
                };
            };

            // Agrega el esquema de seguridad para API Key
            options.AddSecurity("ApiKey", new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "X-API-KEY",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Ingresá tu API Key en el header X-API-KEY"
            });

            // Aplica el esquema a todos los endpoints
            options.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor("ApiKey"));
        });
    }
}