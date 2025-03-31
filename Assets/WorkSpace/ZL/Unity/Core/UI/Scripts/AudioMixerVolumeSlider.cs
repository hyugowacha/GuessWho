using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Audio;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Audio Mixer Volume Slider")]

    [DisallowMultipleComponent]

    public sealed class AudioMixerVolumeSlider : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        [Essential]

        private Slider slider;

        [SerializeField]
        
        [UsingCustomProperty]
        
        [ReadOnlyWhenPlayMode]

        private string key;

        public void Refresh()
        {
            slider.value = ISingleton<AudioMixerManager>.Instance.GetVolume(key) * 100f;
        }

        public void SetVolumeBySliderValue()
        {
            ISingleton<AudioMixerManager>.Instance.SetVolume(key, slider.value * 0.01f);
        }
    }
}