using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Currencies.Queries.Handlers;

public class GetCurrenciesHandler(
    ICurrencyService service,
    ILogger<GetCurrenciesHandler> logger
) : IRequestHandler<GetCurrenciesRequest, IEnumerable<CurrencyResponse>>
{
    private readonly ICurrencyService _service = service;
    private readonly ILogger<GetCurrenciesHandler> _logger = logger;

    public async Task<IEnumerable<CurrencyResponse>> Handle(
        GetCurrenciesRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obteniendo monedas");
        return await _service.GetAll();
    }
}