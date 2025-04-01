using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Audio Source (Poolable)")]

    [RequireComponent(typeof(AudioSource))]

    public sealed class PoolableAudioSource : StopAction<AudioSource>
    {
        public override bool IsStoped => target.isPlaying;

        public float Volume
        {
            get => target.volume;

            set => target.volume = value;
        }
    }
}