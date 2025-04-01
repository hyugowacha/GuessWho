using Photon.Pun;

using Photon.Realtime;

using TMPro;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Room Screen Controller")]

    [DisallowMultipleComponent]

    public sealed class RoomScreenController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private TMP_InputField roomCodeInputField;

        [SerializeField]

        private Button startGameButton;

        private PhotonLobbyManager lobbyManager;

        private Room room;

        private void Start()
        {
            lobbyManager = ISingleton<PhotonLobbyManager>.Instance;
        }

        public void Initialize()
        {
            room = PhotonNetwork.CurrentRoom;

            roomCodeInputField.text = room.Name;

            //startGameButton.interactable = false;
        }

        public void Refresh()
        {
            if (PhotonNetwork.PlayerList.Length >= lobbyManager.MinPlayerCount)
            {
                startGameButton.interactable = true;
            }

            else
            {
                startGameButton.interactable = false;
            }
        }
    }
}