using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Screen (UI) Manager")]

    [DisallowMultipleComponent]

    public class UGUIScreenSwapper : MonoBehaviour
    {
        [Space]

        [SerializeField]

        protected UGUIScreen main;

        public UGUIScreen Current { get; set; } = null;

        public virtual void EnableMainMenu()
        {
            main?.Open();
        }

        public virtual void DisableCurrent()
        {
            Current?.Close();
        }
    }
}