namespace RaspberryPiTest
{
    using System;
    using System.Reactive.Linq;
    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Gpio;

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
                return Observable.FromEvent<ButtonChangedArgs>(
                    callback =>
                    {
                        Pi.Gpio.Pin00.RegisterInterruptCallback(EdgeDetection.FallingEdge, () => callback(new ButtonChangedArgs(false)));
                        Pi.Gpio.Pin00.RegisterInterruptCallback(EdgeDetection.RisingEdge, () => callback(new ButtonChangedArgs(true)));
                    },
                    d =>
                    {
                        // no unsubscribe function
                    });
            }
        }

        private void InitialisePins()
        {
            var pin = Pi.Gpio.Pin00;
            pin.PinMode = GpioPinDriveMode.Input;
        }

        public override string ToString()
        {
            return Pi.Info.ToString();
        }

        public void SetLed(double red, double green, double blue)
        {
            throw new NotImplementedException();
        }
    }
}
