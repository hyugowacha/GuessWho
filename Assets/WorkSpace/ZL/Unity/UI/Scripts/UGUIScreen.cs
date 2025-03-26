using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/UGUI Screen")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(CanvasGroupFader))]

    public class UGUIScreen : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        [GetComponentInParent]

        private UGUIScreenSwapper swapper;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        [GetComponent]

        private CanvasGroupFader fader;

        [Space]

        [SerializeField]

        private UnityEvent eventOnOpen;

        [Space]

        [SerializeField]

        private UnityEvent eventOnClose;

        public virtual void Open()
        {
            if (swapper != null)
            {
                swapper.Current?.Close();

                swapper.Current = this;

                swapper.Last = this;
            }

            fader.SetFaded(true);

            eventOnOpen.Invoke();

            
        }

        public virtual void Close()
        {
            if (swapper != null)
            {
                swapper.Current = null;
            }

            fader.SetFaded(false);

            eventOnClose.Invoke();
        }
    }
}