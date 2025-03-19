using System.Diagnostics;

using UnityEngine;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public class GetComponentAttribute : CustomPropertyAttribute
    {
#if UNITY_EDITOR

        private bool isTypeValid;

        protected override void Initialize(Drawer drawer)
        {
            isTypeValid = drawer.fieldInfo.FieldType.IsSubclassOf(typeof(Component));
        }

        protected override void Preset(Drawer drawer)
        {
            if (isTypeValid == false)
            {
                drawer.DrawErrorBox($"{NameTag} Field type is invalid.");

                return;
            }

            drawer.Property.objectReferenceValue = GetComponent(drawer);
        }

        protected virtual Component GetComponent(Drawer drawer)
        {
            drawer.TargetComponent.TryGetComponent(drawer.fieldInfo.FieldType, out var component);

            return component;
        }

#endif
    }
}