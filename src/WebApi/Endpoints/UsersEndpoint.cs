using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using WebApi.Application.Users.Commands;
using WebApi.Application.Users.Queries;
using WebApi.Domain.Dto;
using WebApi.Domain.Dto.Users;
using WebApi.Infrastructure.Api.Endpoints;

namespace WebApi.Endpoints;

internal class UsersEndpoint : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("v1/api/users", GetUsers)
            .WithTags("Users")
            .Produces<IEnumerable<UserResponse>>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();

        app.MapGet("v1/api/users/{id}", GetUserById)
            .WithTags("Users")
            .Produces<UserResponse>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();

        app.MapPost("v1/api/users", CreateUser)
            .WithTags("Users")
            .Produces<UserResponse>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();

        app.MapPut("v1/api/users/{id}", UpdateUser)
            .WithTags("Users")
            .Produces<UserResponse>((int)HttpStatusCode.OK)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();

        app.MapDelete("v1/api/users/{id}", DeleteUser)
            .WithTags("Users")
            .Produces((int)HttpStatusCode.NoContent)
            .Produces<ErrorResponse>((int)HttpStatusCode.BadRequest)
            .RequireAuthorization();
    }

    [OpenApiOperation("Listar usuarios", "Devuelve todos los usuarios, con filtro opcional por estado.")]
    internal static async Task<IResult> GetUsers(
        IMediator mediator,
        [FromQuery] bool? isActive)
    {
        var response = await mediator.Send(new GetUsersRequest { IsActive = isActive });
        return Results.Ok(response);
    }

    [OpenApiOperation("Obtener usuario", "Devuelve un usuario por su ID.")]
    internal static async Task<IResult> GetUserById(
        IMediator mediator,
        [FromRoute] int id)
    {
        var response = await mediator.Send(new GetUserByIdRequest { Id = id });
        return Results.Ok(response);
    }

    [OpenApiOperation("Crear usuario", "Registra un nuevo usuario en el sistema.")]
    internal static async Task<IResult> CreateUser(
        IMediator mediator,
        [FromBody] CreateUserRequest request)
    {
        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    [OpenApiOperation("Actualizar usuario", "Actualiza los datos de un usuario existente.")]
    internal static async Task<IResult> UpdateUser(
        IMediator mediator,
        [FromRoute] int id,
        [FromBody] UpdateUserRequest request)
    {
        request.Id = id;
        var response = await mediator.Send(request);
        return Results.Ok(response);
    }

    [OpenApiOperation("Eliminar usuario", "Elimina un usuario y sus direcciones asociadas.")]
    internal static async Task<IResult> DeleteUser(
        IMediator mediator,
        [FromRoute] int id)
    {
        await mediator.Send(new DeleteUserRequest { Id = id });
        return Results.NoContent();
    }

    public void DefineServices(IServiceCollection services) { }
}