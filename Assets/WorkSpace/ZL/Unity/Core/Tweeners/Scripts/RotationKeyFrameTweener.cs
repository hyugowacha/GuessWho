using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Rotation Key Frame Tweener")]

    [RequireComponent(typeof(TransformRotationTweener))]

    public sealed class RotationKeyFrameTweener : KeyFrameTweener<TransformRotationTweener, QuaternionTweener, Quaternion, Vector3, QuaternionOptions>
    {
        [Space]

        [SerializeField]

        private RotateMode rotateMode;

        public override void SetKeyFrame(int index)
        {
            transform.rotation = Quaternion.Euler(keyFrames.Current(index));
        }

        protected override TweenerCore<Quaternion, Vector3, QuaternionOptions> TweenKeyFrame()
        {
            return base.TweenKeyFrame().SetRotateMode(rotateMode);
        }
    }
}