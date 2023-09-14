namespace Domain.Models
{
    public class ExchangeModel
    {
        public decimal Amount { get; set; }
        public ISOCurrency MainCurrency { get; set; }
        public ISOCurrency MoneyCurrency { get; set; }
    }
}