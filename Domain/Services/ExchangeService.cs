using Domain.Models;
using Persistence;

namespace Domain.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IFileClient _fileClient;

        public ExchangeService(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }
        public decimal ConvertCurrency(ExchangeModel exchangeModel)
        {
            var currencyRates = _fileClient.ReadFile<CurrencyRate>();

            if (exchangeModel.MainCurrency == exchangeModel.MoneyCurrency)
            {
                return exchangeModel.Amount;
            }

            var mainCurrencyToDKK = exchangeModel.MainCurrency == ISOCurrency.DKK ? 100 : currencyRates
                .FirstOrDefault(x => x.ISO == exchangeModel.MainCurrency.ToString()).Amount;

            var moneyCurrencyToDKK = exchangeModel.MoneyCurrency == ISOCurrency.DKK ? 100 : currencyRates
                .FirstOrDefault(x => x.ISO == exchangeModel.MoneyCurrency.ToString()).Amount;

            return Math.Round(mainCurrencyToDKK / moneyCurrencyToDKK * exchangeModel.Amount, 4);
        }
    }
}