namespace WebApi.Domain.Dto.Currencies;

public class ConvertRequest
{
    public string FromCurrencyCode { get; set; } = string.Empty;
    public string ToCurrencyCode { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}