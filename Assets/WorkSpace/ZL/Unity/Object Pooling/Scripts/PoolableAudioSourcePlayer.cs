using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Audio Source (Poolable) Player")]

    public sealed class PoolableAudioSourcePlayer : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private ObjectPool<PoolableAudioSource> pool;

        public void Play()
        {
            pool.Generate().SetActive(true);
        }
    }
}