using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Audio Listener Volume Tweener (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class AudioListenerVolumeTweener :
        
        ComponentValueTweener<FloatTweener, float, float, FloatOptions>,
        
        ISingleton<AudioListenerVolumeTweener>
    {
        protected override float Value
        {
            get => AudioListener.volume;
            
            set => AudioListener.volume = value;
        }

        protected override void Awake()
        {
            if (ISingleton<AudioListenerVolumeTweener>.TrySetInstance(this) == false)
            {
                return;
            }

            base.Awake();
        }

        private void OnDestroy()
        {
            ISingleton<AudioListenerVolumeTweener>.Release(this);
        }
    }
}