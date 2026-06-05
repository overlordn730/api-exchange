using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Currencies.Commands.Handlers;

public class CreateCurrencyHandler(
    ICurrencyService service,
    ILogger<CreateCurrencyHandler> logger
) : IRequestHandler<CreateCurrencyRequest, CurrencyResponse>
{
    private readonly ICurrencyService _service = service;
    private readonly ILogger<CreateCurrencyHandler> _logger = logger;

    public async Task<CurrencyResponse> Handle(
        CreateCurrencyRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creando moneda {@request}", request);

        return await _service.Create(new CurrencyRequest
        {
            Code = request.Code,
            Name = request.Name,
            RateToBase = request.RateToBase
        });
    }
}