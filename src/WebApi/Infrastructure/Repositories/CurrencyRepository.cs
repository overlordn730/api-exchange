using Microsoft.EntityFrameworkCore;
using WebApi.Application.Exceptions;
using WebApi.Domain.Dto.Currencies;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Data;
using WebApi.Infrastructure.Repositories.Interfaces;

namespace WebApi.Infrastructure.Repositories;

public class CurrencyRepository(OracleDbContext context) : ICurrencyRepository
{
    private readonly OracleDbContext _context = context;

    public async Task<IEnumerable<CurrencyResponse>> GetAll()
    {
        return await _context.Currencies
            .Select(c => new CurrencyResponse
            {
                Id = c.Id,
                Code = c.Code,
                Name = c.Name,
                RateToBase = c.RateToBase
            })
            .ToListAsync();
    }

    public async Task<Currency> Create(Currency currency)
    {
        _context.Currencies.Add(currency);
        await _context.SaveChangesAsync();
        return currency;
    }

    public async Task<Currency?> GetByCode(string code)
    {
        return await _context.Currencies
            .FirstOrDefaultAsync(c => c.Code == code.ToUpper());
    }

    public async Task<bool> CodeExists(string code)
    {
        var count = await _context.Currencies
            .Where(c => c.Code == code.ToUpper())
            .CountAsync();

        return count > 0;
    }
}