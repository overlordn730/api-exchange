namespace WebApi.Application.Exceptions;

public static class Errors
{
    // Códigos de error
    public const string ERR_CLIENT = "400";
    public const string ERR_SERVER = "500";

    // Usuarios
    public const string USER_NOT_FOUND = "Usuario no encontrado";
    public const string EMAIL_ALREADY_EXISTS = "Email ya registrado";

    // Direcciones
    public const string ADDRESS_NOT_FOUND = "Dirección no encontrada";

    // Monedas
    public const string CURRENCY_NOT_FOUND = "Moneda no encontrada";
    public const string CURRENCY_CODE_ALREADY_EXISTS = "Código de moneda ya registrado";
    public const string CURRENCIES_SAME = "Las monedas de origen y destino son iguales";

    // Servidor
    public const string INTERNAL_SERVER_ERROR = "Error interno del servidor";
}