using System.Diagnostics;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class ToggleIfAttribute : FieldConditionAttribute
    {
        public ToggleIfAttribute(string fieldName, bool targetValue) : base(fieldName, targetValue) { }

        public ToggleIfAttribute(string fieldName, object targetValue, bool condition) : base(fieldName, targetValue, condition) { }

#if UNITY_EDITOR

        protected override void SetCondition(Drawer drawer)
        {
            if (targetValue == null)
            {
                drawer.IsToggled = (property.objectReferenceValue == null) == condition;
            }

            else
            {
                drawer.IsToggled = property.boxedValue.Equals(targetValue) == condition;
            }
        }

#endif
    }
}