using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/UGUI Screen Swapper")]

    [DisallowMultipleComponent]

    public class UGUIScreenSwapper : MonoBehaviour
    {
        [Space]

        [SerializeField]

        protected UGUIScreen main;

        public UGUIScreen Current { get; set; } = null;

        public UGUIScreen Last { get; set; } = null;

        public virtual void OpenMainScreen()
        {
            main?.Open();
        }

        public virtual void closeCurrentScreen()
        {
            Current?.Close();
        }

        public virtual void OpenLastScreen()
        {
            Last?.Open();
        }
    }
}