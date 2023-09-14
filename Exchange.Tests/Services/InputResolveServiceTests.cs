using Microsoft.Extensions.Logging;
using Moq;
using Domain.Models;
using Domain.Services;

namespace Exchange.Tests.Services
{
    [TestClass]
    public class InputResolveServiceTests
    {
        private readonly InputResolveService _inputResolveService;
        private readonly Mock<ILogger<InputResolveService>> _logger;

        public InputResolveServiceTests()
        {
            _logger = new Mock<ILogger<InputResolveService>>();
            _inputResolveService = new InputResolveService(_logger.Object);
        }

        [TestMethod]
        public void GetExchangeModel_ValidInput_ReturnsExchangeModel()
        {
            // Arrange
            string input = "Exchange EUR/USD 10";

            // Act
            var exchangeModel = _inputResolveService.GetExchangeModel(input);

            // Assert
            Assert.IsNotNull(exchangeModel);
            Assert.AreEqual(ISOCurrency.EUR, exchangeModel.MainCurrency);
            Assert.AreEqual(ISOCurrency.USD, exchangeModel.MoneyCurrency);
            Assert.AreEqual(10, exchangeModel.Amount);
        }

        [TestMethod]
        public void GetExchangeModel_InvalidInput_StartsWith_And_InputArrayLength_ThrowsArgumentException()
        {
            // Arrange
            string input = "InvalidInput";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _inputResolveService.GetExchangeModel(input));
        }

        [TestMethod]
        public void GetExchangeModel_InvalidCurrencyInput_LowerCase_And_Undefined_ThrowsArgumentException()
        {
            // Arrange
            string input = "Exchange EUR/usd 10";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _inputResolveService.GetExchangeModel(input));
        }

        [TestMethod]
        public void GetExchangeModel_InvalidCurrencyInput_CurrencyPairArray_Length_ThrowsArgumentException()
        {
            // Arrange
            string input = "Exchange EUR/USD/DKK 10";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _inputResolveService.GetExchangeModel(input));
        }

        [TestMethod]
        public void GetExchangeModel_InvalidCurrencyInput_CurrencyPairArray_Element_Length_ThrowsArgumentException()
        {
            // Arrange
            string input = "Exchange EUR/INVALID 10";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _inputResolveService.GetExchangeModel(input));
        }

        [TestMethod]
        public void GetExchangeModel_InvalidAmountInput_ThrowsArgumentException()
        {
            // Arrange
            string input = "Exchange EUR/USD InvalidAmount";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _inputResolveService.GetExchangeModel(input));
        }

        [TestMethod]
        public void GetExchangeModel_EmptyInput_ThrowsArgumentException()
        {
            // Arrange
            string input = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _inputResolveService.GetExchangeModel(input));
        }
    }
}