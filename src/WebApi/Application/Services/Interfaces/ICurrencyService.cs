using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Services.Interfaces;

public interface ICurrencyService
{
    Task<IEnumerable<CurrencyResponse>> GetAll();
    Task<CurrencyResponse> Create(CurrencyRequest request, int userId);
    Task<ConvertResponse> Convert(ConvertRequest request);
}