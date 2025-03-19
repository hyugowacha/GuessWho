using System;

using System.Collections;

using System.Reflection;

namespace ZL.Unity
{
    public static partial class EnumeratorExtensions
    {
        public static IEnumerator Clone(this IEnumerator instance)
        {
            var type = instance.GetType().UnderlyingSystemType;

            var constructor = type.GetConstructor(new Type[] { typeof(int) });

            var clone = (IEnumerator)constructor.Invoke(new object[] { 0 });

            var fields = instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var value = field.GetValue(instance);

                field.SetValue(clone, value);
            }

            fields = instance.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var value = field.GetValue(instance);

                field.SetValue(clone, value);
            }

            return clone;
        }
    }
}