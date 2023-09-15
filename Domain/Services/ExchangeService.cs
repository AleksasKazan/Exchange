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

            var mainCurrencyToDKK = currencyRates.FirstOrDefault(x => x.ISO == exchangeModel.MainCurrency.ToString());
            var moneyCurrencyToDKK = currencyRates.FirstOrDefault(x => x.ISO == exchangeModel.MoneyCurrency.ToString());

            if (mainCurrencyToDKK is null && exchangeModel.MainCurrency != ISOCurrency.DKK || 
                moneyCurrencyToDKK is null && exchangeModel.MoneyCurrency != ISOCurrency.DKK)
            {
                var missingISOCurrency = mainCurrencyToDKK is null && 
                    exchangeModel.MainCurrency != ISOCurrency.DKK ? 
                    exchangeModel.MainCurrency.ToString() : exchangeModel.MoneyCurrency.ToString();
                throw new ArgumentException($"The ISO currency {missingISOCurrency} is valid, but we don't currently have an exchange rate for it.");
            }

            var mainCurrencyToDKKAmount = exchangeModel.MainCurrency == ISOCurrency.DKK ? 100 : mainCurrencyToDKK!.Amount;
            var moneyCurrencyToDKKAmount = exchangeModel.MoneyCurrency == ISOCurrency.DKK ? 100 : moneyCurrencyToDKK!.Amount;

            return Math.Round(mainCurrencyToDKKAmount / moneyCurrencyToDKKAmount * exchangeModel.Amount, 4);
        }
    }
}