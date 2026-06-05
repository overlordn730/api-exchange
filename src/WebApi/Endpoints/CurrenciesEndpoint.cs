using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using WebApi.Application.Currencies.Commands;
using WebApi.Application.Currencies.Queries;
using WebApi.Domain.Dto;
using WebApi.Domain.Dto.Currencies;
using WebApi.Infrastructure.Api.Endpoints;

namespace WebApi.Endpoints;

internal class CurrenciesEndpoint : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("v1/api/currencies", GetCurrencies)
            .WithTags("Currencies")
            .Produces<IEnumerable<CurrencyResponse>>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();

        app.MapPost("v1/api/currencies", CreateCurrency)
            .WithTags("Currencies")
            .Produces<CurrencyResponse>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();

        app.MapPost("v1/api/currency/convert", ConvertCurrency)
            .WithTags("Currencies")
            .Produces<ConvertResponse>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();
    }

    [OpenApiOperation("Listar monedas", "Devuelve todas las monedas registradas en el sistema.")]
    internal static async Task<IResult> GetCurrencies(IMediator mediator)
    {
        var response = await mediator.Send(new GetCurrenciesRequest());
        return Results.Ok(response);
    }

    [OpenApiOperation("Crear moneda", "Registra una nueva moneda con su cotización respecto al Guaraní.")]
    internal static async Task<IResult> CreateCurrency(
        IMediator mediator,
        [FromBody] CreateCurrencyRequest request)
    {
        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    [OpenApiOperation("Convertir divisas", "Convierte un monto de una moneda a otra usando la cotización registrada.")]
    internal static async Task<IResult> ConvertCurrency(
        IMediator mediator,
        [FromBody] ConvertCurrencyRequest request)
    {
        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    public void DefineServices(IServiceCollection services) { }
}