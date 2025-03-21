using UnityEngine;

namespace ZL.Unity
{
    public static partial class UnityEngineObjectExtensions
    {
        public static void Destroy(this Object instance)
        {
            Object.Destroy(instance);
        }

        public static void DontDestroyOnLoad(this Object instance)
        {
            Object.DontDestroyOnLoad(instance);
        }
    }
}