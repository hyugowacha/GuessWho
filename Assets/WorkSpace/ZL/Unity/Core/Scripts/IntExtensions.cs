using UnityEngine;

namespace ZL.Unity
{
    public static partial class IntExtensions
    {
        public static int Clamp(this int instance, int min, int max)
        {
            return Mathf.Clamp(instance, min, max);
        }

        public static int Clamp01(this int instance)
        {
            return Clamp(instance, 0, 1);
        }

        public static int Repeat(this int instance, int min, int max)
        {
            int range = max - min + 1;

            return ((instance - min) % range + range) % range + min;
        }

        public static int PingPong(this int instance, int min, int max)
        {
            int range = max - min;

            int mod = (instance - min) % (2 * range);

            if (mod < 0) mod += 2 * range;

            return mod < range ? min + mod : max - (mod - range);
        }

        public static bool IsOutOfRange(this int instance, int min, int max)
        {
            return min > instance || instance > max;
        }
    }
}