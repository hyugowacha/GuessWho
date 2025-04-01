using UnityEngine;

using ZL.Unity.Audio;

using ZL.Unity.UI;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("")]

    [DisallowMultipleComponent]

    public sealed class SystemOptionController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private AudioMixerVolumeSlider masterVolumeSlider;

        [SerializeField]

        [UsingCustomProperty]

        [Alias("BGM Volume Slider")]

        private AudioMixerVolumeSlider bgmVolumeSlider;

        [SerializeField]

        [UsingCustomProperty]

        [Alias("SFX Volume Slider")]

        private AudioMixerVolumeSlider sfxVolumeSlider;

        public void Refresh()
        {
            masterVolumeSlider.Refresh();

            bgmVolumeSlider.Refresh();

            sfxVolumeSlider.Refresh();
        }

        public void Confirm()
        {
            ISingleton<AudioMixerManager>.Instance.SaveVolumes();
        }

        public void Cancel()
        {
            ISingleton<AudioMixerManager>.Instance.LoadVolumes();
        }
    }
}