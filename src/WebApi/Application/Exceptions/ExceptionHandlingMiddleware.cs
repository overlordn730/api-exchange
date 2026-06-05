using System.Text.Json;
using WebApi.Application.Exceptions;
using WebApi.Domain.Dto;
using ApplicationException = WebApi.Application.Exceptions.ApplicationException;

namespace WebApi.Application.Exceptions;

public sealed class ExceptionHandlingMiddleware(
    ILogger<ExceptionHandlingMiddleware> logger
) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ApiBadRequestException e)
        {
            logger.LogWarning("{Code}: {Message}", e.ErrorCode, e.Message);
            await HandleExceptionAsync(context, e);
        }
        catch (ValidationException e)
        {
            logger.LogWarning("Validation error: {Message}", e.Message);
            await HandleExceptionAsync(context, e);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error: {Message}", e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var errors = GetErrors(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ErrorResponse
        {
            Code = errors?.First().Key ?? Errors.ERR_SERVER,
            Message = string.Join(" | ", errors?.First().Value ?? new[] { Errors.INTERNAL_SERVER_ERROR })
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception)
        => exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

    private static IReadOnlyDictionary<string, string[]>? GetErrors(Exception exception)
        => exception switch
        {
            ValidationException e => e.ErrorsDictionary,
            ApiBadRequestException e => e.ErrorsDictionary,
            ApiException e => e.ErrorsDictionary,
            _ => new Dictionary<string, string[]>
                { { Errors.ERR_SERVER, new[] { exception.Message } } }
        };
}