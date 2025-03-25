using UnityEngine;

using ZL.CS;

namespace ZL.Unity
{
    public static partial class Vector2Extensions
    {
        public static Vector2 Round(this Vector2 instance, int digits)
        {
            instance.x = instance.x.Round(digits);

            instance.y = instance.x.Round(digits);

            return instance;
        }
    }
}