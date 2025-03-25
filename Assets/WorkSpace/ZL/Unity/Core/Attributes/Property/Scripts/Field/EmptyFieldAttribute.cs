using System.Diagnostics;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class EmptyFieldAttribute : FieldAttribute { }
}