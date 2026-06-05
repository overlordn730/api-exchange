namespace WebApi.Application.Exceptions;

public sealed class ValidationException : ApplicationException
{
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }

    public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
        : base("Validation Error", "Se encontraron errores de validación")
    {
        ErrorsDictionary = errorsDictionary;
    }
}