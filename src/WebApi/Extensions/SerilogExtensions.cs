using Serilog;

namespace WebApi.Extensions;

public static class SerilogExtensions
{
    public static void UseCustomSerilogRequestLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = (httpContext, elapsed, ex) =>
            {
                // Health checks en Verbose para no saturar los logs
                if (httpContext.Request.Path.StartsWithSegments("/health"))
                    return Serilog.Events.LogEventLevel.Verbose;

                // Errores en Error
                if (ex != null || httpContext.Response.StatusCode >= 500)
                    return Serilog.Events.LogEventLevel.Error;

                return Serilog.Events.LogEventLevel.Information;
            };
        });
    }
}