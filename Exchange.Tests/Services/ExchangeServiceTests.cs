using Domain.Models;
using Domain.Services;
using Moq;
using Persistence;

namespace Exchange.Tests.Services
{
    [TestClass]
    public class ExchangeServiceTests
    {
        private readonly IFileClient _fileClient;
        private readonly ExchangeService _exchangeService;

        public ExchangeServiceTests()
        {
            var fileClientMock = new Mock<IFileClient>();
            fileClientMock.Setup(f => f.ReadFile<CurrencyRate>())
                          .Returns(new List<CurrencyRate>
                          {
                              new CurrencyRate { ISO = "EUR", Amount = 743.94m },
                              new CurrencyRate { ISO = "USD", Amount = 663.11m }
                          });
            _fileClient = fileClientMock.Object;
            _exchangeService = new ExchangeService(_fileClient);
        }

        [TestMethod]
        public void ConvertCurrency_WithSameCurrencies_ReturnsOriginalAmount()
        {
            // Arrange
            var exchangeModel = new ExchangeModel
            {
                MainCurrency = ISOCurrency.EUR,
                MoneyCurrency = ISOCurrency.EUR,
                Amount = 100m
            };

            // Act
            var result = _exchangeService.ConvertCurrency(exchangeModel);

            // Assert
            Assert.AreEqual(100m, result);
        }

        [TestMethod]
        public void ConvertCurrency_WithMainCurrencyEqualToDKK_ReturnsAmountInEUR()
        {
            // Arrange
            var exchangeModel = new ExchangeModel
            {
                MainCurrency = ISOCurrency.DKK,
                MoneyCurrency = ISOCurrency.EUR,
                Amount = 100m
            };

            // Act
            var result = _exchangeService.ConvertCurrency(exchangeModel);

            // Assert
            Assert.AreEqual(Math.Round(100.0m / 743.94m * 100, 4), result);
        }

        [TestMethod]
        public void ConvertCurrency_WithMoneyCurrencyEqualToDKK_ReturnsAmountInDKK()
        {
            // Arrange
            var exchangeModel = new ExchangeModel
            {
                MainCurrency = ISOCurrency.EUR,
                MoneyCurrency = ISOCurrency.DKK,
                Amount = 100m
            };

            // Act
            var result = _exchangeService.ConvertCurrency(exchangeModel);

            // Assert
            Assert.AreEqual(Math.Round(743.94m / 100.0m * 100m, 4), result);
        }

        [TestMethod]
        public void ConvertCurrency_WithMainCurrencyEqualToEUR_WithMoneyCurrencyEqualToUSD_ReturnsAmountInUSD()
        {
            // Arrange
            var exchangeModel = new ExchangeModel
            {
                MainCurrency = ISOCurrency.EUR,
                MoneyCurrency = ISOCurrency.USD,
                Amount = 100m
            };

            // Act
            var result = _exchangeService.ConvertCurrency(exchangeModel);

            // Assert
            Assert.AreEqual(Math.Round(743.94m / 663.11m * 100m, 4), result);
        }

        [TestMethod]
        public void ConvertCurrency_WithMoneyCurrencyEqualToMissingGBP_ThrowsArgumentException()
        {
            // Arrange
            var exchangeModel = new ExchangeModel
            {
                MainCurrency = ISOCurrency.DKK,
                MoneyCurrency = ISOCurrency.GBP,
                Amount = 100m
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _exchangeService.ConvertCurrency(exchangeModel));
        }
    }
}