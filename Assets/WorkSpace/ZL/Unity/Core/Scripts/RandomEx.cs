using UnityEngine;

namespace ZL.Unity
{
    public static partial class RandomEx
    {
        public static Vector3 Range(in Vector3 min, in Vector3 max)
        {
            return new Vector3
            {
                x = Random.Range(min.x, max.x),

                y = Random.Range(min.y, max.y),

                z = Random.Range(min.z, max.z)
            };
        }

        public static Vector3 Angles()
        {
            return new Vector3
            {
                x = Random.Range(0f, 360f),

                y = Random.Range(0f, 360f),

                z = Random.Range(0f, 360f)
            };
        }
    }
}