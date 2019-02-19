namespace RaspberryPiTest
{
    using System;
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
                throw new NotImplementedException();
            }
        }

        private void InitialisePins()
        {
            Pi.Init<Unosquare.WiringPi.BootstrapWiringPi>();
            Pi.Gpio[504].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[505].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[506].PinMode = GpioPinDriveMode.Output;
        }

        public override string ToString()
        {
            return Pi.Info.ToString();
        }
        private bool isOn = false;
        public void SetLed(double red, double green, double blue)
        {
            Pi.Gpio[504].Write(!isOn);
            Pi.Gpio[505].Write(!isOn);
            Pi.Gpio[506].Write(!isOn);
            isOn = !isOn;
        }
    }
}
