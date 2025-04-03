using ExitGames.Client.Photon.StructWrapping;

using Photon.Pun;

using TMPro;

using UnityEngine;

using UnityEngine.UI;

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

        private int minPlayers;

        public void Initialize()
        {
            var room = PhotonNetwork.CurrentRoom;

            roomCodeInputField.text = room.Name;

            startGameButton.interactable = false;

            room.CustomProperties.TryGetValue("minPlayers", out int minPlayers);

            this.minPlayers = minPlayers;

            Refresh();
        }

        public void Refresh()
        {
            if (PhotonNetwork.IsMasterClient == false)
            {
                return;
            }

            startGameButton.interactable = PhotonNetwork.PlayerList.Length >= minPlayers;
        }
    }
}