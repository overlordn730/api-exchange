using System.Reflection;
using Serilog;
using WebApi.Extensions;
using WebApi.Infrastructure;
using WebApi.Infrastructure.Api.Endpoints;
using WebApi.Application.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// 1. Logging
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

// 2. Autenticación
builder.Services.AddApiKeyAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// 3. Endpoints (autodescubrimiento por reflexión)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointDefinitions(typeof(Program));

// 4. Swagger/OpenAPI
builder.Services.AddSwaggerConfiguration();

// 5. Infraestructura (repositorios, servicios, mappers, DbContext)
builder.Services.AddInfrastructure(builder.Configuration);

// 6. MediatR (CQRS)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// 7. CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 8. HttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ── Pipeline HTTP ──
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCustomSerilogRequestLogging();
app.UseEndpointDefinitions();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();