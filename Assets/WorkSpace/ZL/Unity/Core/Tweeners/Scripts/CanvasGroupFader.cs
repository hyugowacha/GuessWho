using DG.Tweening;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Tweeners;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Canvas Group Fader")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(CanvasGroupAlphaTweener))]

    public class CanvasGroupFader : MonoBehaviour
    {
        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Toggle(true)]

        private CanvasGroup canvasGroup;

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Toggle(true)]

        private CanvasGroupAlphaTweener alphaTweener;

        [Space]

        [SerializeField]

        private bool isFadedIn = false;

        public bool IsFadedIn
        {
            get => isFadedIn;

            set
            {
                isFadedIn = value;

                if (isFadedIn == true)
                {
                    gameObject.SetActive(true);

                    canvasGroup.alpha = 1f;
                }

                else
                {
                    canvasGroup.alpha = 0f;

                    gameObject.SetActive(false);
                }
            }
        }

        [Space]

        [SerializeField]

        private UnityEvent eventOnFadeIn;

        [Space]

        [SerializeField]

        private UnityEvent eventOnFadeOut;

        private bool interactable;

        private void Awake()
        {
            IsFadedIn = isFadedIn;
        }

        public void SetFaded(bool value)
        {
            SetFaded(value, alphaTweener.Tweener.Duration);
        }

        public void SetFaded(bool value, float duration)
        {
            isFadedIn = value;

            if (isFadedIn == true)
            {
                gameObject.SetActive(true);

                alphaTweener.Tween(1f, duration).OnComplete(OnFadedIn);
            }

            else
            {
                interactable = canvasGroup.interactable;

                canvasGroup.interactable = false;

                alphaTweener.Tween(0f, duration).OnComplete(OnFadedOut);
            }
        }

        protected virtual void OnFadedIn()
        {
            eventOnFadeIn.Invoke();
        }

        protected virtual void OnFadedOut()
        {
            eventOnFadeOut.Invoke();

            gameObject.SetActive(false);

            canvasGroup.interactable = interactable;
        }
    }
}