using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/UGUI Screen Group")]

    [DisallowMultipleComponent]

    public sealed class UGUIScreenGroup : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UGUIScreen main;

        [Space]

        [SerializeField]

        private bool fadeOutCurrent = true;

        [SerializeField]

        private bool sortSibling = true;

        private UGUIScreen current = null;

        public void FadeInMain()
        {
            main?.FadeIn();
        }

        public void SwapCurrent(UGUIScreen newCurrent)
        {
            if (fadeOutCurrent == true)
            {
                FadeOutCurrent();
            }

            current = newCurrent;

            if (sortSibling == true)
            {
                current.transform.SetAsLastSibling();
            }
        }

        public void FadeOutCurrent()
        {
            current?.FadeOut();
        }
    }
}