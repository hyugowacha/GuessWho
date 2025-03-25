using UnityEngine;

namespace ZL.Unity
{
    public static class LayerExtensions
    {
        public static bool Contains(this LayerMask instance, int layer)
        {
            return (instance.value & (1 << layer)) != 0;
        }
    }
}