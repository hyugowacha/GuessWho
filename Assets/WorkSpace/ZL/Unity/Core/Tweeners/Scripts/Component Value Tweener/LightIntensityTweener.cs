using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Light Intensity Tweener")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(Light))]

    public sealed class LightIntensityTweener : ComponentValueTweener<FloatTweener, float, float, FloatOptions>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        [GetComponent]

#pragma warning disable CS0108

        private Light light;

#pragma warning restore CS0108

        protected override float Value
        {
            get => light.intensity;
            
            set => light.intensity = value;
        }
    }
}