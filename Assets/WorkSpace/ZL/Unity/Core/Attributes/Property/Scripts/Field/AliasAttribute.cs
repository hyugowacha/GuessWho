using System.Diagnostics;

namespace ZL.Unity
{
    [Conditional("UNITY_EDITOR")]

    public sealed class AliasAttribute : CustomPropertyAttribute
    {
        public string Text { get; set; }

        public AliasAttribute(string text)
        {
            Text = text;
        }

#if UNITY_EDITOR

        protected override void Draw(Drawer drawer)
        {
            drawer.PropertyLabel.text = Text;
        }

#endif
    }
}