using System;

using System.Diagnostics;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class SetIndentAttribute : CustomPropertyAttribute
    {
        private readonly int level;

        public SetIndentAttribute(int level)
        {
            this.level = level;
        }

#if UNITY_EDITOR

        protected override void Draw(Drawer drawer)
        {
            drawer.IndentLevel = level;
        }

#endif
    }
}