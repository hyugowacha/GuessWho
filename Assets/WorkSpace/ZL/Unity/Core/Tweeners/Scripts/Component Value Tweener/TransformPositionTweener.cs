using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Transform Position Tweener")]

    [DisallowMultipleComponent]

    public sealed class TransformPositionTweener :
        
        ComponentValueTweener<Vector3Tweener, Vector3, Vector3, VectorOptions>
    {
        protected override Vector3 Value
        {
            get => transform.position;
            
            set => transform.position = value;
        }
    }
}