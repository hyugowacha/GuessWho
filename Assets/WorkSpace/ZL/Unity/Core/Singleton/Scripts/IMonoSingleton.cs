using UnityEngine;

namespace ZL.Unity
{
    public interface IMonoSingleton<T> : ISingleton<T>
        
        where T : MonoBehaviour, ISingleton<T>
    {
        bool ISingleton<T>.TrySetInstance()
        {
            if (IsDuplicated() == true)
            {
                ((T)this).Destroy();

                return false;
            }

            Instance = (T)this;

            ((T)this).DontDestroyOnLoad();

            return true;
        }
    }
}