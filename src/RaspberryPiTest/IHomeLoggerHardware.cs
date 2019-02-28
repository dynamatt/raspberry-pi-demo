﻿namespace RaspberryPiTest
{
    using System;
    using System.IO;
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

            try
            {
                File.WriteAllText(@"/sys/class/gpio/export", "504");
                File.WriteAllText(@"/sys/class/gpio/export", "505");
                File.WriteAllText(@"/sys/class/gpio/export", "506");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try
            {
                File.WriteAllText(@"/sys/class/gpio/gpio504/direction", "out");
                File.WriteAllText(@"/sys/class/gpio/gpio505/direction", "out");
                File.WriteAllText(@"/sys/class/gpio/gpio506/direction", "out");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

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

            int r = red > 0.5 ? 1 : 0;
            int g = green > 0.5 ? 1 : 0;
            int b = blue > 0.5 ? 1 : 0;

            try
            {
                File.WriteAllText(@"/sys/class/gpio/gpio504/value", r.ToString());
                File.WriteAllText(@"/sys/class/gpio/gpio505/value", g.ToString());
                File.WriteAllText(@"/sys/class/gpio/gpio506/value", b.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            isOn = !isOn;
        }
    }
}
