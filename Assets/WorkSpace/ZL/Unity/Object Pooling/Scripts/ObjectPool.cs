using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public class ObjectPool<TComponent> : Pool<TComponent>

        where TComponent : Component
    {
        [SerializeField]

        protected TComponent original;

        public TComponent Original => original;

        [SerializeField]

        private Transform parent;

        public Transform Parent => parent;

        public void PreGenerate(int count)
        {
            while (count-- > 0)
            {
                Replicate().SetActive(false);
            }
        }

        public override TComponent Replicate()
        {
            return PooledObject.Replicate(this);
        }
    }
}