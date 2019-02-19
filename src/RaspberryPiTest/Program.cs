namespace RaspberryPiTest
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddOptions();
                services.Configure<HomeLoggerConfig>(hostContext.Configuration.GetSection("Daemon"));

                services.AddSingleton<IHomeLoggerHardware, HomeLoggerHardware>();
                services.AddSingleton<IHostedService, HomeLoggerService>();
            })
            .ConfigureLogging((hostingContext, logging) => 
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
            });

            await builder.RunConsoleAsync();
        }
    }
}
