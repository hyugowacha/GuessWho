using TMPro;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Slider Value Displayer")]

    [DisallowMultipleComponent]

    public class SliderValueDisplayer : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        [Essential]

        protected Slider slider;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        [Essential]

        private TextMeshProUGUI valueText;

        public virtual void OnValueChanged()
        {
            valueText.text = slider.value.ToString();
        }
    }
}