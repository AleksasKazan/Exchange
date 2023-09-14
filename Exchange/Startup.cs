using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Exchange
{
    public class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var fileName = config["FileClientConfig:FileName"];

            services.AddSingleton<IFileClient>(new FileClient(fileName));

            services.AddSingleton<ExchangeApp>()
                    .AddSingleton<IExchangeService, ExchangeService>()
                    .AddSingleton<IInputResolveService, InputResolveService>()
                    .AddLogging(builder =>
                    {
                        builder.AddConsole();
                    });

            return services.BuildServiceProvider();
        }
    }
}