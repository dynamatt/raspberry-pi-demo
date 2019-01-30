namespace RaspberryPiTest
{
    using System;

    public enum Colours
    {
        Red,
        Orange,
        Green,
        Blue,
    }

    public static class HardwareExtensions
    {
        public static void SetLed(this IHomeLoggerHardware hardware, Colours color)
        {
            switch (color)
            {
                case Colours.Red:
                    hardware.SetLed(1, 0, 0);
                    return;
                case Colours.Green:
                    hardware.SetLed(0, 1, 0);
                    return;
                case Colours.Blue:
                    hardware.SetLed(0, 0, 1);
                    return;
            }
            throw new NotImplementedException($"The colour {color} has not been implemented.");
        }

        public static IObservable<TimeSpan> ButtonPressedAndReleased(IObservable<ButtonChangedArgs> buttonChanged)
        {
            throw new NotImplementedException();
        }
    }
}
