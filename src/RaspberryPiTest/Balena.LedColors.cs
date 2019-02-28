namespace Balena
{
    using System.Collections.Generic;

    public class LedColor
    {
        internal LedColor(bool red, bool green, bool blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public bool Red { get; }
        public bool Green { get; }
        public bool Blue { get; }
    }

    public static class LedColors
    {
        public static readonly LedColor Off = new LedColor(false, false, false);
        public static readonly LedColor Blue = new LedColor(false, false, true);
        public static readonly LedColor Green = new LedColor(false, true, false);
        public static readonly LedColor Orange = new LedColor(false, true, true);
        public static readonly LedColor Red = new LedColor(true, false, false);
        public static readonly LedColor Purple = new LedColor(true, false, true);
        public static readonly LedColor Yellow = new LedColor(true, true, false);
        public static readonly LedColor White = new LedColor(true, true, true);

        public static IEnumerable<LedColor> Colors
        {
            get
            {
                yield return Blue;
                yield return Green;
                yield return Orange;
                yield return Red;
                yield return Purple;
                yield return Yellow;
                yield return White;
            }
        }
    }
}
