namespace RaspberryPiTest
{
    using System;
    using System.Diagnostics;
    using System.Reactive.Linq;
    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Abstractions;

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
        void SetLed(double red, double green, double blue);
    }

    public class HomeLoggerHardware : IHomeLoggerHardware
    {
        public HomeLoggerHardware()
        {
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
            //Pi.Gpio[504].PinMode = GpioPinDriveMode.Output;
            //Pi.Gpio[505].PinMode = GpioPinDriveMode.Output;
            //Pi.Gpio[506].PinMode = GpioPinDriveMode.Output;

            //var process = new Process();
            //process.StartInfo.FileName =
            //"echo 504 > /sys/class/gpio/export # (Red)\n" +
            //"echo 505 > /sys/class/gpio/export # (Green)\n" +
            //"echo 506 > /sys/class/gpio/export # (Blue)\n" +
            //"echo \"out\" > /sys/class/gpio/gpio504/direction\n" +
            //"echo \"out\" > /sys/class/gpio/gpio505/direction\n" +
            //"echo \"out\" > /sys/class/gpio/gpio506/direction\n";
            //process.Start();
        }

        public override string ToString()
        {
            return Pi.Info.ToString();
        }

        private bool isOn = false;
        public void SetLed(double red, double green, double blue)
        {
            //Pi.Gpio[504].Write(!isOn);
            //Pi.Gpio[505].Write(!isOn);
            //Pi.Gpio[506].Write(!isOn);
            var process = new Process();
            int r = red > 0 ? 1 : 0;
            int g = green > 0 ? 1 : 0;
            int b = blue > 0 ? 1 : 0;
            //process.StartInfo.FileName =
            //$"echo {r} > / sys /class/gpio/gpio504/value\n" +
            //$"echo {g} > /sys/class/gpio/gpio505/value\n" +
            //$"echo {b} > /sys/class/gpio/gpio506/value";
            //process.Start();
            isOn = !isOn;
        }
    }
}
