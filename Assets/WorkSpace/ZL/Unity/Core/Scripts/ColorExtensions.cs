using UnityEngine;

namespace ZL.Unity
{
    public static partial class ColorExtensions
    {
        public static Color ToneDown(this Color instance, in float tone)
        {
            instance.r *= tone;

            instance.g *= tone;

            instance.b *= tone;

            return instance;
        }
    }
}