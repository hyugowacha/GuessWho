using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("")]

    [DisallowMultipleComponent]

    public sealed class PoolCollector : MonoBehaviour
    {
        private Action actionOnDisable;

        private Action actionOnDestroy;

        public static T Clone<T>(ObjectPool<T> pool)

            where T : Component
        {
            T clone = Instantiate(pool.Original, pool.Parent);

            var poolCollector = clone.AddComponent<PoolCollector>();

            poolCollector.actionOnDisable = () => pool.Collect(clone);

            poolCollector.actionOnDestroy = () => pool.Release(clone);

            return clone;
        }

        private void OnDisable()
        {
            actionOnDisable.Invoke();
        }

        private void OnDestroy()
        {
            actionOnDestroy.Invoke();
        }
    }
}