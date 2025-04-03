using ExitGames.Client.Photon;

using Photon.Pun;

using Photon.Realtime;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Room Creation Controller")]

    [DisallowMultipleComponent]

    public sealed class RoomCreationController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private Slider maxPlayersSlider;

        [SerializeField]

        private Toggle publicRoomToggle;

        private Hashtable roomProperties;

        private void Awake()
        {
            roomProperties = new()
            {
                ["minPlayers"] = (int)maxPlayersSlider.minValue
            };
        }

        public void CreateRoom()
        {
            RoomOptions roomOptions = new()
            {
                MaxPlayers = (int)maxPlayersSlider.value,

                IsVisible = publicRoomToggle.isOn,
            };

            ISingleton<PhotonLobbyManager>.Instance.CreateRoom(null, roomOptions);
        }

        public void UpdateRoomProperties()
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
        }
    }
}