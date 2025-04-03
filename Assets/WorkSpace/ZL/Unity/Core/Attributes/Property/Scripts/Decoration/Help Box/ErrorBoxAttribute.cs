using System.Diagnostics;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class ErrorBoxAttribute : MessageBoxAttribute
    {
        public ErrorBoxAttribute(string message) : base(message)
        {

        }

#if UNITY_EDITOR

        protected override void Draw(Drawer drawer)
        {
            drawer.DrawErrorBox(message);
        }

#endif
    }
}