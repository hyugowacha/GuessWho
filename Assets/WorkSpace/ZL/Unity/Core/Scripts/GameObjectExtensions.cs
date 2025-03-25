using System;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity
{
    public static partial class GameObjectExtensions
    {
        #region GetComponentInChildren

        public static bool TryGetComponentInChildren<TComponent>(this GameObject instance, out TComponent result)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentInChildren(out result);
        }

        public static bool TryGetComponentInChildren(this GameObject instance, Type type, out Component result)
        {
            return instance.transform.TryGetComponentInChildren(type, out result);
        }

        public static bool TryGetComponentInChildrenOnly<TComponent>(this GameObject instance, out TComponent result)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentInChildrenOnly(out result);
        }

        public static bool TryGetComponentInChildrenOnly(this GameObject instance, Type type, out Component result)
        {
            return instance.transform.TryGetComponentInChildrenOnly(type, out result);
        }

        public static bool TryGetComponentsInChildren<TComponent>(this GameObject instance, out List<TComponent> result)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentsInChildren(out result);
        }

        public static bool TryGetComponentsInChildrenOnly<TComponent>(this GameObject instance, out List<TComponent> result)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentsInChildrenOnly(out result);
        }

        #endregion

        #region GetComponentInParent

        public static bool TryGetComponentInParent<TComponent>(this GameObject instance, out TComponent result)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentInParent(out result);
        }

        public static bool TryGetComponentInParent(this GameObject instance, Type type, out Component result)
        {
            return instance.transform.TryGetComponentInParent(type, out result);
        }

        public static bool TryGetComponentInParentOnly<TComponent>(this GameObject instance, out TComponent result)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentInParentOnly(out result);
        }

        public static bool TryGetComponentInParentOnly(this GameObject instance, Type type, out Component result)
        {
            return instance.transform.TryGetComponentInParentOnly(type, out result);
        }

        public static bool TryGetComponentsInParent<TComponent>(this GameObject instance, out List<TComponent> result)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentsInParent(out result);
        }

        public static bool TryGetComponentsInParentOnly<TComponent>(this GameObject instance, out List<TComponent> result)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentsInParentOnly(out result);
        }

        #endregion

        public static GameObject AddComponents(this GameObject instance, params Type[] types)
        {
            foreach (var component in types)
            {
                instance.AddComponent(component);
            }

            return instance;
        }
    }
}