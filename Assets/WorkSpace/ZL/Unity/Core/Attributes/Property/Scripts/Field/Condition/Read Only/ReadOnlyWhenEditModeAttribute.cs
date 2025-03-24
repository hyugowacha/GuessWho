using System.Diagnostics;

using UnityEngine;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class ReadOnlyWhenEditModeAttribute : CustomPropertyAttribute
    {
#if UNITY_EDITOR

        protected override void Draw(Drawer drawer)
        {
            drawer.IsEnabled = Application.isPlaying;
        }

#endif
    }
}