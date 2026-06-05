using WebApi.Infrastructure.Api.Endpoints;

namespace WebApi.Infrastructure.Api.Endpoints;

public static class EndpointDefinitionExtensions
{
    public static void AddEndpointDefinitions(
        this IServiceCollection services,
        params Type[] scanMarkers)
    {
        var endpointDefinitions = new List<IEndpointDefinition>();

        foreach (var marker in scanMarkers)
        {
            endpointDefinitions.AddRange(
                marker.Assembly.GetTypes()
                    .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsInterface)
                    .Select(Activator.CreateInstance)
                    .Cast<IEndpointDefinition>());
        }

        services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
    }

    public static void UseEndpointDefinitions(this WebApplication app)
    {
        var definitions = app.Services
            .GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

        foreach (var definition in definitions)
        {
            definition.DefineEndpoints(app);
        }
    }
}