using System;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public sealed class ManagedObjectPool<T> : ObjectPool<T>

        where T : Component
    {
        private readonly HashSet<T> clones = new();

        public override T Generate()
        {
            var clone = base.Generate();

            clones.Add(clone);

            return clone;
        }

        public void Recall()
        {
            foreach (var clone in clones)
            {
                clone.gameObject.SetActive(false);
            }

            clones.Clear();
        }

        public void Clear()
        {
            foreach (var clone in clones)
            {
                clone.gameObject.Destroy();
            }

            clones.Clear();
        }
    }
}