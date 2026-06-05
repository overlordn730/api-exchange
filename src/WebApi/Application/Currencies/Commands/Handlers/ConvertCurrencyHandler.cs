using MediatR;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Currencies.Commands.Handlers;

public class ConvertCurrencyHandler(
    ICurrencyService service,
    ILogger<ConvertCurrencyHandler> logger
) : IRequestHandler<ConvertCurrencyRequest, ConvertResponse>
{
    private readonly ICurrencyService _service = service;
    private readonly ILogger<ConvertCurrencyHandler> _logger = logger;

    public async Task<ConvertResponse> Handle(
        ConvertCurrencyRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Convirtiendo {@request}", request);

        return await _service.Convert(new ConvertRequest
        {
            FromCurrencyCode = request.FromCurrencyCode,
            ToCurrencyCode = request.ToCurrencyCode,
            Amount = request.Amount
        });
    }
}