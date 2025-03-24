using System.Diagnostics;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class PropertyFieldAttribute : FieldAttribute
    {
#if UNITY_EDITOR

        protected override void Draw(Drawer drawer)
        {
            drawer.DrawPropertyField();
        }

#endif
    }
}