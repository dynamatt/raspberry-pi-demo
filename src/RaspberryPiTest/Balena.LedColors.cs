namespace Balena
{
    public class LedColor
    { 
        public bool Red { get; internal set; }
        public bool Green { get; internal set; }
        public bool Blue { get; internal set; }
    }

    public class LedColors
    {
        public static readonly LedColor Red = new LedColor { Red = true };
        public static readonly LedColor Yellow = new LedColor { Red = true, Green = true };
        public static readonly LedColor Green = new LedColor { Green = true };
        public static readonly LedColor Blue = new LedColor { Blue = true };
        public static readonly LedColor Purple = new LedColor { Red = true, Blue = true };
    }
}
