using System;

using System.Collections.Generic;

#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

using Object = UnityEngine.Object;

namespace ZL.Unity
{
    public static partial class ComponentExtensions
    {
        #region GetComponentInChildren

        public static bool TryGetComponentInChildren<T>(this Component instance, out T result)

            where T : Component
        {
            return instance.transform.TryGetComponentInChildren(out result);
        }

        public static bool TryGetComponentInChildren(this Component instance, Type type, out Component result)
        {
            return instance.transform.TryGetComponentInChildren(type, out result);
        }

        public static bool TryGetComponentInChildrenOnly<T>(this Component instance, out T result)

            where T : Component
        {
            return instance.transform.TryGetComponentInChildrenOnly(out result);
        }

        public static bool TryGetComponentInChildrenOnly(this Component instance, Type type, out Component result)
        {
            return instance.transform.TryGetComponentInChildrenOnly(type, out result);
        }

        public static bool TryGetComponentsInChildren<T>(this Component instance, out List<T> result)

            where T : Component
        {
            return instance.transform.TryGetComponentsInChildren(out result);
        }

        public static bool TryGetComponentsInChildrenOnly<T>(this Component instance, out List<T> result)

            where T : Component
        {
            return instance.transform.TryGetComponentsInChildrenOnly(out result);
        }

        #endregion

        #region GetComponentInParent

        public static bool TryGetComponentInParent<T>(this Component instance, out T result)

            where T : Component
        {
            return instance.transform.TryGetComponentInParent(out result);
        }

        public static bool TryGetComponentInParent(this Component instance, Type type, out Component result)
        {
            return instance.transform.TryGetComponentInParent(type, out result);
        }

        public static bool TryGetComponentInParentOnly<T>(this Component instance, out T result)

            where T : Component
        {
            return instance.transform.TryGetComponentInParentOnly(out result);
        }

        public static bool TryGetComponentInParentOnly(this Component instance, Type type, out Component result)
        {
            return instance.transform.TryGetComponentInParentOnly(type, out result);
        }

        public static bool TryGetComponentsInParent<T>(this Component instance, out List<T> result)

            where T : Component
        {
            return instance.transform.TryGetComponentsInParent(out result);
        }

        public static bool TryGetComponentsInParentOnly<T>(this Component instance, out List<T> result)

            where T : Component
        {
            return instance.transform.TryGetComponentsInParentOnly(out result);
        }

        #endregion

        public static T AddComponent<T>(this Component instance)

            where T : Component
        {
            return instance.gameObject.AddComponent<T>();
        }

        public static GameObject AddComponents<T>(this T instance, params Type[] components)

            where T : Component
        {
            return instance.gameObject.AddComponents(components);
        }

        public static T SetActive<T>(this T instance, bool value)

            where T : Component
        {
            instance.gameObject.SetActive(value);

            return instance;
        }

        public static void DisallowMultiple<T>(this T instance)

            where T : Component
        {
            void DestroyImmediate()
            {
                instance.DestroyImmediate();

                string typeName = instance.GetType().Name;

                FixedEditorUtility.DisplayDialog("Invalid operation.", $"Can't add '{typeName}' to {instance.gameObject.name} because a '{typeName}' is already added to the game object!", "Ok");
            }

            if (instance.GetType().IsInheritGeneric(out var type) == true)
            {
                Component[] components = instance.GetComponents<Component>();

                foreach (var component in components)
                {
                    if (component == instance)
                    {
                        continue;
                    }

                    if (component.GetType().IsInheritGeneric(out var compareType) == true)
                    {
                        if (type.GetGenericTypeDefinition() == compareType.GetGenericTypeDefinition())
                        {
                            DestroyImmediate();

                            return;
                        }
                    }
                }
            }

            else if (instance.GetComponents<T>().Length > 1)
            {
                DestroyImmediate();
            }
        }

        public static void DestroyImmediate<T>(this T instance)

            where T : Component
        {
#if UNITY_EDITOR

            if (Application.isPlaying == false)
            {
                void Callback()
                {
                    var gameObject = instance.gameObject;

                    Object.DestroyImmediate(instance);

                    EditorUtility.SetDirty(gameObject);

                    EditorApplication.update -= Callback;
                }

                EditorApplication.update += Callback;

                return;
            }

#endif

            Object.DestroyImmediate(instance);
        }
    }
}