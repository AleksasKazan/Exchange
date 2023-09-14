using Domain.Models;

namespace Domain.Services
{
    public interface IExchangeService
    {
        decimal ConvertCurrency(ExchangeModel exchangeModel);
    }
}