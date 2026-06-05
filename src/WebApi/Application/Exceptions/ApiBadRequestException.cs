namespace WebApi.Application.Exceptions;

public sealed class ApiBadRequestException : BadRequestException
{
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }

    public ApiBadRequestException(string errorCode, string message)
        : base(message, errorCode)
    {
        ErrorsDictionary = new Dictionary<string, string[]>
        {
            { errorCode, new[] { message } }
        };
    }
}