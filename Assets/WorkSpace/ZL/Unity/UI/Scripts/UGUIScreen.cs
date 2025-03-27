using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/UGUI Screen")]

    [DisallowMultipleComponent]

    public class UGUIScreen : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        [GetComponentInParent]

        private UGUIScreenSwapper swapper;

        public virtual void Open()
        {
            if (swapper != null)
            {
                swapper.Current?.Close();

                swapper.Current = this;

                swapper.Last = this;
            }
        }

        public virtual void Close()
        {
            if (swapper != null)
            {
                swapper.Current = null;
            }
        }
    }
}