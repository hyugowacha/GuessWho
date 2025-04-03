using TMPro;

using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Input Field Text Receiver")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(TMP_InputField))]

    public sealed class InputFieldTextReceiver : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private TMP_InputField inputField;

        [Space]

        [SerializeField]

        private UnityEvent<string> eventOnRecieve;

        public void Receive()
        {
            eventOnRecieve.Invoke(inputField.text);
        }
    }
}