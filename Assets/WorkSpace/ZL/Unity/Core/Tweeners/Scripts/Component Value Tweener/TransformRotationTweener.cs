using DG.Tweening;

using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [AddComponentMenu("ZL/Tweeners/Transform Rotation Tweener")]

    [DisallowMultipleComponent]

    public sealed class TransformRotationTweener :
        
        ComponentValueTweener<QuaternionTweener, Quaternion, Vector3, QuaternionOptions>
    {
        [Space]

        [SerializeField]

        private RotateMode rotateMode;

        protected override Quaternion Value
        {
            get => transform.rotation;

            set => transform.rotation = value;
        }
    }
}