using Photon.Pun;

using TMPro;

using UnityEngine;

using ZL.Unity.UI;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Player Nickname Controller")]

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
            if (PhotonNetwork.NickName.IsValidNickname() == false)
            {
                screen.FadeIn();
            }
        }

        public void Refresh()
        {
            var nickname = PhotonNetwork.NickName;

            nicknameInputField.text = nickname;

            cancelButton.SetActive(nickname.IsValidNickname());
        }

        public void TryConfirm()
        {
            if (ISingleton<PhotonServerManager>.Instance.TrySetNickname(nicknameInputField.text, out var exception) == false)
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