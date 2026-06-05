using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using WebApi.Application.Addresses.Commands;
using WebApi.Application.Addresses.Queries;
using WebApi.Domain.Dto;
using WebApi.Domain.Dto.Addresses;
using WebApi.Infrastructure.Api.Endpoints;

namespace WebApi.Endpoints;

internal class AddressesEndpoint : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("v1/api/users/{userId}/addresses", GetAddressesByUser)
            .WithTags("Addresses")
            .Produces<IEnumerable<AddressResponse>>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();

        app.MapPost("v1/api/users/{userId}/addresses", CreateAddress)
            .WithTags("Addresses")
            .Produces<AddressResponse>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();

        app.MapPut("v1/api/addresses/{id}", UpdateAddress)
            .WithTags("Addresses")
            .Produces<AddressResponse>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();

        app.MapDelete("v1/api/addresses/{id}", DeleteAddress)
            .WithTags("Addresses")
            .Produces((int)HttpStatusCode.NoContent)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();
    }

    [OpenApiOperation("Listar direcciones", "Devuelve todas las direcciones de un usuario.")]
    internal static async Task<IResult> GetAddressesByUser(
        IMediator mediator,
        [FromRoute] int userId)
    {
        var response = await mediator.Send(new GetAddressesByUserRequest { UserId = userId });
        return Results.Ok(response);
    }

    [OpenApiOperation("Crear dirección", "Registra una nueva dirección para un usuario.")]
    internal static async Task<IResult> CreateAddress(
        IMediator mediator,
        [FromRoute] int userId,
        [FromBody] CreateAddressRequest request)
    {
        request.UserId = userId;
        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    [OpenApiOperation("Actualizar dirección", "Actualiza los datos de una dirección existente.")]
    internal static async Task<IResult> UpdateAddress(
        IMediator mediator,
        [FromRoute] int id,
        [FromBody] UpdateAddressRequest request)
    {
        request.Id = id;
        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    [OpenApiOperation("Eliminar dirección", "Elimina una dirección por su ID.")]
    internal static async Task<IResult> DeleteAddress(
        IMediator mediator,
        [FromRoute] int id)
    {
        await mediator.Send(new DeleteAddressRequest { Id = id });
        return Results.NoContent();
    }

    public void DefineServices(IServiceCollection services) { }
}