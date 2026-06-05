using MediatR;
using WebApi.Domain.Dto.Currencies;

namespace WebApi.Application.Currencies.Queries;

public class GetCurrenciesRequest : IRequest<IEnumerable<CurrencyResponse>>
{
}