using UnityEngine;

using UnityEngine.Audio;

#if UNITY_EDITOR

using UnityEditor;

#endif

using ZL.Unity.Collections;

using ZL.Unity.IO;

namespace ZL.Unity.Audio
{
    [AddComponentMenu("ZL/Audio/Audio Mixer Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class AudioMixerManager
        
        : MonoBehaviour, IMonoSingleton<AudioMixerManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Essential]

        [Button("LoadAudioMixerParameters", "Load Parameters")]

        private AudioMixer audioMixer = null;

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Button(nameof(LoadVolumes))]

        [Button(nameof(SaveVolumes))]

        private SerializableDictionary<string, float, FloatPref> parameterPrefs;

#if UNITY_EDITOR

        private void Reset()
        {
            LoadAudioMixerParameters();
        }

        private void OnValidate()
        {
            foreach (var parameterPref in parameterPrefs)
            {
                parameterPref.Value = Mathf.Clamp01(parameterPref.Value);

                if (Application.isPlaying == true)
                {
                    SetVolume(parameterPref.Key, parameterPref.Value);
                }
            }
        }

        public void LoadAudioMixerParameters()
        {
            parameterPrefs.Clear();

            if (audioMixer != null)
            {
                foreach (var audioMixerGroup in audioMixer.FindMatchingGroups(string.Empty))
                {
                    var key = audioMixerGroup.name;

                    audioMixer.GetFloat(key, out float value);

                    FloatPref volumePref = new(key, value);

                    volumePref.TryLoadValue();

                    parameterPrefs.Add(volumePref);
                }
            }

            EditorUtility.SetDirty(this);
        }

#endif

        private void Awake()
        {
            ISingleton<AudioMixerManager>.TrySetInstance(this);
        }

        private void Start()
        {
            foreach (var parameterPref in parameterPrefs)
            {
                parameterPref.ActionOnValueChanged += (value) =>
                {
                    audioMixer.SetVolume(parameterPref.Key, value);
                };
            }

            LoadVolumes();
        }

        private void OnDestroy()
        {
            ISingleton<AudioMixerManager>.Release(this);
        }

        public void SaveVolumes()
        {
            foreach (var parameterPref in parameterPrefs)
            {
                parameterPref.SaveValue();
            }
        }

        public void LoadVolumes()
        {
            foreach (var parameterPref in parameterPrefs)
            {
                parameterPref.TryLoadValue();
            }
        }

        public float GetVolume(string key)
        {
            return parameterPrefs[key];
        }

        public void SetVolume(string key, float value)
        {
            parameterPrefs[key] = value;
        }
    }
}