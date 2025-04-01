using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Forced Layout Rebuilder")]

    [DisallowMultipleComponent]

    public sealed class ForcedLayoutRebuilder : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        [PropertyField]

        [ReadOnly(false)]

        [Button(nameof(ForceRebuildLayout))]

        private RectTransform rectTransform;

        private void Start()
        {
            ForceRebuildLayout();
        }

        public void ForceRebuildLayout()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }
}