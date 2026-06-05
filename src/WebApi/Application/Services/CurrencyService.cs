using WebApi.Application.Exceptions;
using WebApi.Application.Mapper;
using WebApi.Application.Services.Interfaces;
using WebApi.Domain.Dto.Currencies;
using WebApi.Infrastructure.Repositories.Interfaces;

namespace WebApi.Application.Services;

public class CurrencyService(
    ICurrencyRepository repository,
    CurrencyMapper mapper,
    ILogger<CurrencyService> logger
) : ICurrencyService
{
    private readonly ICurrencyRepository _repository = repository;
    private readonly CurrencyMapper _mapper = mapper;
    private readonly ILogger<CurrencyService> _logger = logger;

    public async Task<IEnumerable<CurrencyResponse>> GetAll()
    {
        _logger.LogInformation("Obteniendo monedas");
        return await _repository.GetAll();
    }

    public async Task<CurrencyResponse> Create(CurrencyRequest request)
    {
        _logger.LogInformation("Creando moneda {@request}", request);

        if (await _repository.CodeExists(request.Code))
            throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.CURRENCY_CODE_ALREADY_EXISTS);

        var currency = _mapper.MapToEntity(request);
        currency.Code = request.Code.ToUpper();

        var created = await _repository.Create(currency);
        return _mapper.MapToResponse(created);
    }

    public async Task<ConvertResponse> Convert(ConvertRequest request)
    {
        _logger.LogInformation("Convirtiendo {@request}", request);

        var from = await _repository.GetByCode(request.FromCurrencyCode)
            ?? throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.CURRENCY_NOT_FOUND);

        var to = await _repository.GetByCode(request.ToCurrencyCode)
            ?? throw new ApiBadRequestException(Errors.ERR_CLIENT, Errors.CURRENCY_NOT_FOUND);

        // Fórmula de conversión
        var amountInBase = request.Amount * from.RateToBase;
        var convertedAmount = amountInBase / to.RateToBase;

        return new ConvertResponse
        {
            FromCurrency = from.Code,
            ToCurrency = to.Code,
            OriginalAmount = request.Amount,
            ConvertedAmount = Math.Round(convertedAmount, 2)
        };
    }
}