using Domain.Models;

namespace Domain.Services
{
    public interface IInputResolveService
    {
        ExchangeModel GetExchangeModel(string input);
    }
}