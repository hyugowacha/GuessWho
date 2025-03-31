using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Rect Transform Anchored Position Tweener")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(RectTransform))]

    public sealed class RectTransformAnchoredPositionTweener : ComponentValueTweener<Vector2Tweener, Vector2, Vector2, VectorOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private RectTransform rectTransform;

        protected override Vector2 Value
        {
            get => rectTransform.anchoredPosition;

            set => rectTransform.anchoredPosition = value;
        }
    }
}