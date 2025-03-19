using System;

namespace ZL.CS
{
    public static partial class FloatExtensions
    {
        public static float Round(this float instance)
        {
            return MathF.Round(instance);
        }

        public static float Round(this float instance, int digits)
        {
            return (float)Math.Round(instance, digits);
        }
    }
}