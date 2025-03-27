using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Scale Key Frame Tweener")]

    [RequireComponent(typeof(TransformScaleTweener))]

    public sealed class ScaleKeyFrameTweener :
        
        KeyFrameTweener<TransformScaleTweener, Vector3Tweener, Vector3, Vector3, VectorOptions>
    {
        public override void SetKeyFrame(int index)
        {
            keyFrames.Index = index;

            if (keyFrames.TryGetCurrent(out var result) == true)
            {
                transform.localScale = result;
            }
        }
    }
}