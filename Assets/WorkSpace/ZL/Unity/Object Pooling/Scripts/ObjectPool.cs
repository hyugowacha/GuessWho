using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public class ObjectPool<T> : Pool<T>

        where T : Component
    {
        [SerializeField]

        protected T original;

        public T Original => original;

        [SerializeField]

        private Transform parent;

        public Transform Parent => parent;

        public void PreGenerate(int count)
        {
            while (count-- > 0)
            {
                Clone().SetActive(false);
            }
        }

        public override T Clone()
        {
            return PoolCollector.Clone(this);
        }
    }
}