using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Scale Key Frame Tweener")]

    [RequireComponent(typeof(TransformScaleTweener))]

    public sealed class ScaleKeyFrameTweener : KeyFrameTweener<TransformScaleTweener, Vector3Tweener, Vector3, Vector3, VectorOptions>
    {
        public override void SetKeyFrame(int index)
        {
            transform.localScale = keyFrames.Current(index);
        }
    }
}