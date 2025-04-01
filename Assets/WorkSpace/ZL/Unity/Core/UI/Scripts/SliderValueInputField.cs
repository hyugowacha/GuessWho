using TMPro;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Slider Value Input Field")]

    [DisallowMultipleComponent]

    public sealed class SliderValueInputField : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private Slider slider;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private TMP_InputField valueInputField;

        public void Refresh()
        {
            valueInputField.text = slider.value.ToString();
        }

        public void SetValueByInput()
        {
            if (float.TryParse(valueInputField.text, out float result) == true)
            {
                slider.value = result;
            }
        }
    }
}