using TMPro;

using UnityEngine;

using ZL.Unity.Server.Photon;

using ZL.Unity.UI;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(UGUIScreen))]

    public sealed class PlayerNicknameController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private UGUIScreen screen;

        [Space]

        [SerializeField]

        private TMP_InputField nicknameInputField;

        [SerializeField]

        private GameObject cancelButton;

        public void TryInitializeNickname()
        {
            if (ISingleton<PhotonPlayerManager>.Instance.Nickname.IsValidNickname() == false)
            {
                screen.FadeIn();
            }
        }

        public void Refresh()
        {
            var nickname = ISingleton<PhotonPlayerManager>.Instance.Nickname;

            nicknameInputField.text = nickname;

            cancelButton.SetActive(nickname.IsValidNickname());
        }

        public void TryConfirm()
        {
            if (ISingleton<PhotonPlayerManager>.Instance.TrySetNickname(nicknameInputField.text, out var exception) == false)
            {
                switch (exception)
                {
                    case NicknameValidationException.NullOrEmpty:



                        return;
                }
            }
            
            screen.FadeOut();
        }
    }
}