namespace WebApi.Domain.Dto.Currencies;

public class CurrencyRequest
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal RateToBase { get; set; }
}