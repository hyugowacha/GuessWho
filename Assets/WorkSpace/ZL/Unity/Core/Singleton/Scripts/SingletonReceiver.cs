using UnityEngine;

namespace ZL.Unity
{
    [DisallowMultipleComponent]

    public abstract class SingletonReceiver<T> : MonoBehaviour

    where T : MonoBehaviour, ISingleton<T>
    {
        protected T Instance => ISingleton<T>.Instance;
    }
}