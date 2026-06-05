using MediatR;
using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Currencies.Commands;

public class CreateCurrencyRequest : IRequest<CurrencyResponse>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal RateToBase { get; set; }
}