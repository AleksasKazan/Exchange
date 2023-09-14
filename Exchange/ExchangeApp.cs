using Domain.Services;
using Microsoft.Extensions.Logging;

namespace Exchange
{
    public class ExchangeApp
    {
        private readonly IExchangeService _exchangeService;
        private readonly IInputResolveService _inputResolveService;
        private readonly ILogger<ExchangeApp> _logger;

        public ExchangeApp(IExchangeService exchangeService, IInputResolveService inputResolveService, ILogger<ExchangeApp> logger)
        {
            _exchangeService = exchangeService;
            _inputResolveService = inputResolveService;
            _logger = logger;
        }

        public void Start()
        {
            ConsoleKeyInfo cki;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Currency Exchange Calculator");
                Console.WriteLine("1. Start currency exchange calculation");
                Console.WriteLine("2. Exit");

                cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Console.WriteLine("Usage: Enter the 'Exchange <currency pair> <amount to exchange>' to exchange (e.g., Exchange EUR/DKK 1)");
                        var input = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.ReadKey();
                            break;
                        }

                        try
                        {
                            var exchangeModel = _inputResolveService.GetExchangeModel(input);
                            var exchangedAmount = _exchangeService.ConvertCurrency(exchangeModel);

                            Console.WriteLine($"Exchanged amount: {exchangedAmount}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "An error occurred during currency conversion.");
                            Console.WriteLine("An error occurred during currency conversion. Please try again.");
                        }

                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}