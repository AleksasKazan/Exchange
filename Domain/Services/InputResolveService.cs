using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Domain.Services
{
    public class InputResolveService : IInputResolveService
    {
        private readonly ILogger<InputResolveService> _logger;

        public InputResolveService(ILogger<InputResolveService> logger)
        {
            _logger = logger;
        }

        public ExchangeModel GetExchangeModel(string input)
        {
            var exchangeModel = new ExchangeModel();

            try
            {
                if (!input.StartsWith("Exchange"))
                {
                    throw new ArgumentException("Unrecognized command");
                }

                var inputArray = input.Split(' ');

                if (inputArray.Length != 3)
                {
                    throw new ArgumentException("Invalid input format, try e.g. 'Exchange EUR/DKK 1'");
                }

                var currencyPair = inputArray[1];
                var currencyPairArray = currencyPair.Split('/');

                if (currencyPairArray.Length != 2 ||
                    currencyPairArray[0].Length != 3 ||
                    currencyPairArray[1].Length != 3)
                {
                    throw new ArgumentException($"Invalid Currency input format '{currencyPair}', try e.g. 'EUR/DKK'");
                }

                var isValidMainCurrency = Enum.TryParse(currencyPairArray[0], out ISOCurrency mainCurrency);
                var isValidMoneyCurrency = Enum.TryParse(currencyPairArray[1], out ISOCurrency moneyCurrency);

                if (!isValidMainCurrency && !isValidMoneyCurrency)
                {
                    throw new ArgumentException($"The currency pair contains unknown currencies: '{currencyPairArray[0]}' and '{currencyPairArray[1]}'");
                }

                if (!isValidMainCurrency || !isValidMoneyCurrency)
                {
                    var unknownCurrency = isValidMainCurrency ? currencyPairArray[1] : currencyPairArray[0];
                    throw new ArgumentException($"The currency pair contains an unknown currency: '{unknownCurrency}'");
                }

                exchangeModel.MainCurrency = mainCurrency;
                exchangeModel.MoneyCurrency = moneyCurrency;

                if (!decimal.TryParse(inputArray[2], out decimal amount))
                {
                    throw new ArgumentException("Invalid Amount input");
                }

                exchangeModel.Amount = amount;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while parsing input.");
                throw;
            }

            return exchangeModel;
        }
    }
}