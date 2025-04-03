using System;

using System.Diagnostics;

namespace ZL.Unity
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]

    [Conditional("UNITY_EDITOR")]

    public sealed class UsingCustomPropertyAttribute : CustomPropertyAttribute
    {

    }
}