namespace WebApi.Application.Exceptions;

public class ApiException : ApplicationException
{
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }

    public ApiException(string errorCode, string message)
        : base("Internal Server Error", message)
    {
        ErrorsDictionary = new Dictionary<string, string[]>
        {
            { errorCode, new[] { message } }
        };
    }
}