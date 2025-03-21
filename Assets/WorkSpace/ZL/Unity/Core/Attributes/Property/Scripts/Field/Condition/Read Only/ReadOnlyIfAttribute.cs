using System.Diagnostics;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class ReadOnlyIfAttribute : FieldConditionAttribute
    {
        public ReadOnlyIfAttribute(string fieldName, bool targetValue) : base(fieldName, targetValue, true) { }

        public ReadOnlyIfAttribute(string fieldName, object targetValue, bool condition) : base(fieldName, targetValue, condition) { }

#if UNITY_EDITOR

        protected override void SetCondition(Drawer drawer)
        {
            if (targetValue == null)
            {
                drawer.IsEnabled = (property.objectReferenceValue == null) == condition;

                return;
            }

            drawer.IsEnabled = property.boxedValue.Equals(targetValue) == condition;
        }

#endif
    }
}