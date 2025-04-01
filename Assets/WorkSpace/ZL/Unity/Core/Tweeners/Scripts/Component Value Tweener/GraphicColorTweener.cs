using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Unity/Tweeners/Graphic Color Tweener")]

    [DisallowMultipleComponent]

    public sealed class GraphicColorTweener :
        
        ComponentValueTweener<ColorTweener, Color, Color, ColorOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private Graphic graphic;

        protected override Color Value { get => graphic.color; set => graphic.color = value; }
    }
}