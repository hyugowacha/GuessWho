using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Transform Scale Tweener")]

    [DisallowMultipleComponent]

    public sealed class TransformScaleTweener :
        
        ComponentValueTweener<Vector3Tweener, Vector3, Vector3, VectorOptions>
    {
        protected override Vector3 Value
        {
            get => transform.localScale;
            
            set => transform.localScale = value;
        }
    }
}