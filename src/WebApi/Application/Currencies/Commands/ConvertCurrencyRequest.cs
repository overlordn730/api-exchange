using MediatR;
using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Currencies.Commands;

public class ConvertCurrencyRequest : IRequest<ConvertResponse>
{
    public string FromCurrencyCode { get; set; } = string.Empty;
    public string ToCurrencyCode { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}