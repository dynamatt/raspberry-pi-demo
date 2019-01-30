namespace RaspberryPiTest
{
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

        public HomeLoggerService(ILogger<HomeLoggerService> logger, 
            IOptions<HomeLoggerConfig> config,
            IHomeLoggerHardware hardware)
        {
            _logger = logger;
            _config = config;
            _hardware = hardware;

            _hardware.ButtonChanged.Do(args =>
            {
                _logger.LogInformation($"Button: {args.IsPressed}");
            });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting home logger service");
            _logger.LogInformation(_hardware.ToString());

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping home logger service");
            throw new System.NotImplementedException();
        }
    }
}
