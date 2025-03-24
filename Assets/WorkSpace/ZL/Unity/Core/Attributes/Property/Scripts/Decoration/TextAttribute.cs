using System.Diagnostics;

using UnityEngine;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class TextAttribute : CustomPropertyAttribute
    {
        private readonly GUIContent label;

        private readonly GUIStyle style;

        public int FontSize { get; set; } = defaultFontSize;

        public bool RichText { get; set; } = true;

        public float Height { get; set; } = 0f;

        public TextAttribute(string text, TextAnchor alignment = TextAnchor.UpperLeft)
        {
            label = new(text);

            style = new()
            {
                alignment = alignment,

                richText = true,
            };

            style.normal.textColor = Color.gray;
        }

#if UNITY_EDITOR

        protected override void Initialize(Drawer drawer)
        {
            style.fontSize = FontSize;

            style.richText = RichText;

            if (Height == 0f)
            {
                Height = style.CalcSize(label).y;
            }
        }

        protected override void Draw(Drawer drawer)
        {
            drawer.DrawText(Height, label, style);
        }

#endif
    }
}