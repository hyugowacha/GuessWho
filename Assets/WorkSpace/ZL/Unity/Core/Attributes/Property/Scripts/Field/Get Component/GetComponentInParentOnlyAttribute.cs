using System.Diagnostics;

using UnityEngine;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class GetComponentInParentOnlyAttribute : GetComponentAttribute
    {
#if UNITY_EDITOR

        protected override Component GetComponent(Drawer drawer)
        {
            drawer.TargetComponent.TryGetComponentInParentOnly(drawer.fieldInfo.FieldType, out var component);

            return component;
        }

#endif
    }
}