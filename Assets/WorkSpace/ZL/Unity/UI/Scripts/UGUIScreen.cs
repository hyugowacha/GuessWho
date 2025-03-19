using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Screen (UI)")]

    [DisallowMultipleComponent]

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

        public virtual void Open()
        {
            if (swapper != null)
            {
                swapper.Current?.Close();

                swapper.Current = this;
            }

            if (fader != null)
            {
                fader.SetFaded(true);
            }

            else
            {
                gameObject.SetActive(true);
            }
        }

        public virtual void Close()
        {
            if (swapper != null)
            {
                swapper.Current = null;
            }

            if (fader != null)
            {
                fader.SetFaded(false);
            }

            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}