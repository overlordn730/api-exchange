using WebApi.Domain.Dto.Currencies;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Repositories.Interfaces;

public interface ICurrencyRepository
{
    Task<IEnumerable<CurrencyResponse>> GetAll();
    Task<Currency> Create(Currency currency);
    Task<Currency?> GetByCode(string code);
    Task<bool> CodeExists(string code);
}