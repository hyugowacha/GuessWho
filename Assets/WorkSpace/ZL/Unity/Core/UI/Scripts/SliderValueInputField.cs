using TMPro;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Slider Value Input Field")]

    [DisallowMultipleComponent]

    [ExecuteInEditMode]

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

#if UNITY_EDITOR

        private string inputText = null;

        private float sliderValue = 0;

        private void Awake()
        {
            if (valueInputField != null)
            {
                inputText = valueInputField.text;
            }

            if (slider != null)
            {
                sliderValue = slider.value;
            }
        }

        private void Update()
        {
            if (Application.isPlaying == true)
            {
                return;   
            }

            if (valueInputField != null && slider != null)
            {
                if (inputText != valueInputField.text)
                {
                    SetValueByInput();
                }

                else if (sliderValue != slider.value)
                {
                    Refresh();
                }

                inputText = valueInputField.text;

                sliderValue = slider.value;
            }
        }

#endif

        public void Refresh()
        {
            valueInputField.text = slider.value.ToString();
        }

        public void SetValueByInput()
        {
            if (float.TryParse(valueInputField.text, out float value) == true)
            {
                slider.value = value;
            }

            Refresh();
        }
    }
}