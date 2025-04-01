using DG.Tweening;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Tweeners;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/UGUI Screen")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(CanvasGroupAlphaTweener))]

    public sealed class UGUIScreen : MonoBehaviour
    {
        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [EmptyField]

        private CanvasGroup canvasGroup;

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [EmptyField]

        private CanvasGroupAlphaTweener alphaTweener;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInParentOnly]

        [ReadOnly(true)]

        private UGUIScreenGroup screenGroup;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [ReadOnlyWhenEditMode]

        [Button(nameof(ToggleFaded))]

        private bool isFadedIn = false;

        public bool IsFadedIn => isFadedIn;

        [Space]

        [SerializeField]

        private UnityEvent eventOnFadingIn;

        [Space]

        [SerializeField]

        private UnityEvent eventOnFadedIn;

        [Space]

        [SerializeField]

        private UnityEvent eventOnFadingOut;

        [Space]

        [SerializeField]

        private UnityEvent eventOnFadedOut;

        private void Awake()
        {
            if (isFadedIn == true)
            {
                canvasGroup.alpha = 1f;

                gameObject.SetActive(true);
            }

            else
            {
                canvasGroup.alpha = 0f;

                gameObject.SetActive(false);
            }
        }

        public void ToggleFaded()
        {
            if (isFadedIn == true)
            {
                FadeOut();
            }

            else
            {
                FadeIn();
            }
        }

        public void FadeIn()
        {
            screenGroup?.SwapCurrent(this);

            gameObject.SetActive(true);

            isFadedIn = true;

            eventOnFadingIn.Invoke();

            alphaTweener.Tween(1f).OnComplete(OnFadedIn);
        }

        private void OnFadedIn()
        {
            eventOnFadedIn.Invoke();
        }

        public void FadeOut()
        {
            isFadedIn = false;

            eventOnFadingOut.Invoke();

            alphaTweener.Tween(0f).OnComplete(OnFadedOut);
        }

        private void OnFadedOut()
        {
            eventOnFadedOut.Invoke();

            gameObject.SetActive(false);
        }
    }
}