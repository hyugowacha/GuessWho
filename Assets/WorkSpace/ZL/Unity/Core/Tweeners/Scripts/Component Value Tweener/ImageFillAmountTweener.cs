using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Image Fill Amount Tweener")]

    [DisallowMultipleComponent]
    
    [RequireComponent(typeof(Image))]

    public sealed class ImageFillAmountTweener :
        
        ComponentValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        [GetComponent]

        private Image image;

        protected override float Value { get => image.fillAmount; set => image.fillAmount = value; }
    }
}