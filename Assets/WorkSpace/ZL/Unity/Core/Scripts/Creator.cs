using System;

using UnityEngine;

namespace ZL.Unity
{
    public static partial class Creator
    {
        public static TComponent Create<TComponent>(Transform parent, params Type[] components)

           where TComponent : Component
        {
            var name = typeof(TComponent).Name.ToTitleCase();

            return Create<TComponent>(name, parent, components);
        }

        public static TComponent Create<TComponent>(string name, Transform parent, params Type[] components)

           where TComponent : Component
        {
            var component = Create<TComponent>(name, parent);

            component.AddComponents(components);

            return component;
        }

        public static TComponent Create<TComponent>(string name, Transform parent)

           where TComponent : Component
        {
            var gameObject = CreateGameObject(name, parent);

            return gameObject.AddComponent<TComponent>();
        }

        public static GameObject CreateGameObject(string name, Transform parent, params Type[] components)
        {
            var gameObject = CreateGameObject(name, components);

            gameObject.transform.SetParent(parent, false);

            return gameObject;
        }

        public static GameObject CreateGameObject(string name, params Type[] components)
        {
            GameObject gameObject = new(name, components);

            FixedUndo.RegisterCreatedObjectUndo(gameObject, $"Create {gameObject.name}");

            return gameObject;
        }
    }
}