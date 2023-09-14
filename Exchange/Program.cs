using Exchange;
using Microsoft.Extensions.DependencyInjection;

var startup = new Startup();

var serviceProvider = startup.ConfigureServices();

var exchangeApp = serviceProvider.GetService<ExchangeApp>();

exchangeApp?.Start();