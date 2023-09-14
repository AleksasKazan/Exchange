namespace Domain.Models
{
    public class CurrencyRate
    {
        public string Currency { get; init; }
        public string ISO { get; init; }
        public decimal Amount { get; init; }
    }
}