# Api.Exchange
API REST desarrollada en .NET 10 con Minimal APIs para gestión de usuarios, direcciones y conversión de divisas.

## Stack tecnológico
- .NET 10
- Minimal APIs
- CQRS con MediatR
- Entity Framework Core 9 + Oracle
- FluentValidation
- Serilog
- NSwag (OpenAPI/Swagger)
- Mapperly
- BCrypt.Net

## Requisitos previos
- .NET 10 SDK
- Oracle Database 12c o superior

## Cómo correr el proyecto
1. Clonar el repositorio
2. Ejecutar el script SQL en Oracle:
scripts/create_tables.sql
3. Completar las credenciales en `src/WebApi/appsettings.Development.json`:
```json
   {
     "ConnectionStrings": {
       "OracleDbContext": "User Id=TU_USUARIO;Password=TU_PASSWORD;Data Source=localhost:1521/ORCLPDB;"
     },
     "ApiKeyConfiguration": {
       "Header": "X-API-KEY",
       "Realm": "api-exchange",
       "Key": "TU_API_KEY"
     }
   }
```
4. Ejecutar la API:
```bash
   dotnet run --project src/WebApi/WebApi.csproj
```
5. Abrir Swagger en:
https://localhost:7294/swagger

## Autenticación
Todos los endpoints requieren el header `X-API-KEY`.

En Swagger usar el botón **Authorize** e ingresar la API Key configurada en `appsettings.Development.json`.

## Endpoints

### Users
| Método | Ruta | Descripción |
|---|---|---|
| GET | /v1/api/users | Listar usuarios |
| GET | /v1/api/users/{id} | Obtener usuario por ID |
| POST | /v1/api/users | Crear usuario |
| PUT | /v1/api/users/{id} | Actualizar usuario |
| DELETE | /v1/api/users/{id} | Eliminar usuario |

### Addresses
| Método | Ruta | Descripción |
|---|---|---|
| GET | /v1/api/users/{userId}/addresses | Listar direcciones de un usuario |
| POST | /v1/api/users/{userId}/addresses | Crear dirección |
| PUT | /v1/api/addresses/{id} | Actualizar dirección |
| DELETE | /v1/api/addresses/{id} | Eliminar dirección |

### Currencies
| Método | Ruta | Descripción |
|---|---|---|
| GET | /v1/api/currencies | Listar monedas |
| POST | /v1/api/currencies | Crear moneda |
| POST | /v1/api/currency/convert | Convertir divisas |

## Fórmula de conversión
montoBase       = amount * from.RateToBase
convertedAmount = montoBase / to.RateToBase
La moneda base es el **Guaraní Paraguayo (PYG)** con RateToBase = 1.

## Qué está implementado
- ✅ CRUD de Users
- ✅ CRUD de Addresses (relación 1:N con Users)
- ✅ CRUD de Currencies
- ✅ Conversión de divisas
- ✅ Autenticación por API Key
- ✅ FluentValidation
- ✅ CQRS con MediatR
- ✅ Swagger/OpenAPI
- ✅ Logging con Serilog
- ✅ Oracle Database