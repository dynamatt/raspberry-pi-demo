namespace RaspberryPiTest
{
    using System;
    using System.IO;
    using System.Reactive.Linq;
    using Balena;
    using Microsoft.Extensions.Logging;
    using Unosquare.RaspberryIO;

    public struct ButtonChangedArgs
    {
        public ButtonChangedArgs(bool isPressed)
        {
            IsPressed = isPressed;
        }

        public bool IsPressed { get; }
    }

    public interface IHomeLoggerHardware
    {
        IObservable<ButtonChangedArgs> ButtonChanged { get; }
        void SetLed(LedColor color);
    }

    public class HomeLoggerHardware : IHomeLoggerHardware
    {
        private readonly ILogger<HomeLoggerHardware> _logger;

        public HomeLoggerHardware(ILogger<HomeLoggerHardware> logger)
        {
            this._logger = logger;
            InitialisePins();
        }

        public IObservable<ButtonChangedArgs> ButtonChanged
        {
            get
            {
                return Observable.Empty<ButtonChangedArgs>();
            }
        }

        private void InitialisePins()
        {
            Pi.Init<Unosquare.WiringPi.BootstrapWiringPi>();

            try
            {
                File.WriteAllText(@"/sys/class/gpio/export", "504");
                File.WriteAllText(@"/sys/class/gpio/export", "505");
                File.WriteAllText(@"/sys/class/gpio/export", "506");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception thrown while initialising LED pins for export.");
            }

            try
            {
                File.WriteAllText(@"/sys/class/gpio/gpio504/direction", "out");
                File.WriteAllText(@"/sys/class/gpio/gpio505/direction", "out");
                File.WriteAllText(@"/sys/class/gpio/gpio506/direction", "out");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception thrown while setting LED pin direction.");
            }

        }

        public override string ToString()
        {
            return Pi.Info.ToString();
        }

        public void SetLed(LedColor color)
        {
            File.WriteAllText(@"/sys/class/gpio/gpio504/value", color.Red ? "1" : "0");
            File.WriteAllText(@"/sys/class/gpio/gpio505/value", color.Green ? "1" : "0");
            File.WriteAllText(@"/sys/class/gpio/gpio506/value", color.Blue ? "1" : "0");

        }
    }
}
