namespace RaspberryPiTest
{
    using System;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class HomeLoggerService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IOptions<HomeLoggerConfig> _config;
        private readonly IHomeLoggerHardware _hardware;
        private readonly Timer _timer;

        public HomeLoggerService(ILogger<HomeLoggerService> logger, 
            IOptions<HomeLoggerConfig> config,
            IHomeLoggerHardware hardware)
        {
            _logger = logger;
            _config = config;
            _hardware = hardware;
            _timer = new Timer(ChangeLeds);
            _hardware.ButtonChanged.Do(args =>
            {
                _logger.LogInformation($"Button: {args.IsPressed}");
            });
        }

        void ChangeLeds(object state)
        {
            Console.WriteLine($"{DateTime.Now} Changing LEDs");
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting home logger service");
            _logger.LogInformation(_hardware.ToString());

            _timer.Change(0, 3000);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping home logger service");
            _timer.Change(Timeout.Infinite, Timeout.Infinite);

            throw new System.NotImplementedException();
        }
    }
}
