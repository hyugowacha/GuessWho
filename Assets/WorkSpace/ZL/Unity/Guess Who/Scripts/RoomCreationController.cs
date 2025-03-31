using Photon.Realtime;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("")]

    [DisallowMultipleComponent]

    public sealed class RoomCreationController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private Slider maxPlayerCountSlider;

        [SerializeField]

        private Toggle publicRoomToggle;

        private void Start()
        {
            var photonLobbyManager = ISingleton<PhotonLobbyManager>.Instance;

            maxPlayerCountSlider.minValue = photonLobbyManager.MinPlayerCount;

            maxPlayerCountSlider.maxValue = photonLobbyManager.MaxPlayerCount;

            maxPlayerCountSlider.value = photonLobbyManager.MaxPlayerCount;
        }

        public void CreateRoom()
        {
            RoomOptions roomOptions = new()
            {
                MaxPlayers = (int)maxPlayerCountSlider.value,

                IsVisible = publicRoomToggle.isOn,
            };

            ISingleton<PhotonLobbyManager>.Instance.CreateRoom(roomOptions);
        }
    }
}