namespace RaspberryPiTest
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Balena;
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

        private int ledCount = 0;
        void ChangeLeds(object state)
        {
            Console.WriteLine($"{DateTime.Now} Changing LEDs");
            var color = LedColors.Colors.ToArray()[ledCount];
            if (ledCount++ > LedColors.Colors.Count())
            {
                ledCount = 0;
            }
            _hardware.SetLed(color);
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
            return  Task.CompletedTask;
        }
    }
}
