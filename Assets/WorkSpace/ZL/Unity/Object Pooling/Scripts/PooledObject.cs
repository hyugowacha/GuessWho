using System;

using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Pooled Object")]

    [DisallowMultipleComponent]

    public class PooledObject : MonoBehaviour
    {
        private Action actionOnDisable;

        private Action actionOnDestroy;

        public static TReplica Replicate<TReplica>(ObjectPool<TReplica> pool)

            where TReplica : Component
        {
            var replica = Instantiate(pool.Original, pool.Parent);

            if (replica.TryGetComponent<PooledObject>(out var pooledObject) == false)
            {
                FixedDebug.LogWarning($"The '{pool.Original.name}' prefab being pooled doesn't have a component of type 'PooledObject'. We recommend adding it to the prefab to improve performance.");

                pooledObject = replica.AddComponent<PooledObject>();
            }

            pooledObject.actionOnDisable = () => pool.Collect(replica);

            pooledObject.actionOnDestroy = () => pool.Release(replica);

            return replica;
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