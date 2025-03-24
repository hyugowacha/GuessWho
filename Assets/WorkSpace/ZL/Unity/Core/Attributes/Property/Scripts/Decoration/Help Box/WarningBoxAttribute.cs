using System.Diagnostics;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class WarningBoxAttribute : MessageBoxAttribute
    {
        public WarningBoxAttribute(string message) : base(message) { }


#if UNITY_EDITOR

        protected override void Draw(Drawer drawer)
        {
            drawer.DrawWarningBox(message);
        }

#endif
    }
}