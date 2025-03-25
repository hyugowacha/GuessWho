using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Transform Local Position Tweener")]

    [DisallowMultipleComponent]

    public sealed class TransformLocalPositionTweener :
        
        ComponentValueTweener<Vector3Tweener, Vector3, Vector3, VectorOptions>
    {
        protected override Vector3 Value
        {
            get => transform.localPosition;

            set => transform.localPosition = value;
        }
    }
}