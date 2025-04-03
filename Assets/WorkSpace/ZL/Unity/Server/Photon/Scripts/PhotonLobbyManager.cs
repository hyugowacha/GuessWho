using ExitGames.Client.Photon;

using Photon.Pun;

using Photon.Realtime;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Lobby Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PhotonLobbyManager
        
        : MonoBehaviourPunCallbacks, ISingleton<PhotonLobbyManager>
    {
        [Space]

        [SerializeField]

        private UnityEvent eventOnJoinedLobby;

        public UnityEvent EventOnJoinedLobby => eventOnJoinedLobby;

        [Space]

        [SerializeField]

        private UnityEvent evenOnLeftLobby;

        public UnityEvent EvenOnLeftLobby => evenOnLeftLobby;

        [Space]

        [SerializeField]

        private UnityEvent eventOnCreatedRoom;

        public UnityEvent EventOnCreatedRoom => eventOnCreatedRoom;

        [Space]

        [SerializeField]

        private UnityEvent<short> eventOnCreateRoomFailed;

        public UnityEvent<short> EventOnCreateRoomFailed => eventOnCreateRoomFailed;

        [Space]

        [SerializeField]

        private UnityEvent<List<RoomInfo>> eventOnRoomListUpdate;

        public UnityEvent<List<RoomInfo>> EventOnRoomListUpdate => eventOnRoomListUpdate;

        [Space]

        [SerializeField]

        private UnityEvent<Hashtable> eventOnRoomPropertiesUpdate;

        public UnityEvent<Hashtable> EventOnRoomPropertiesUpdate => eventOnRoomPropertiesUpdate;

        private void Awake()
        {
            ISingleton<PhotonLobbyManager>.TrySetInstance(this);
        }

        private void OnDestroy()
        {
            ISingleton<PhotonLobbyManager>.Release(this);
        }

        public override void OnJoinedLobby()
        {
            FixedDebug.Log($"Joined lobby: {PhotonNetwork.CurrentLobby.Name}");

            eventOnJoinedLobby.Invoke();
        }

        public void LeaveLobby()
        {
            PhotonNetwork.LeaveLobby();
        }

        public override void OnLeftLobby()
        {
            FixedDebug.Log($"Left lobby");

            evenOnLeftLobby.Invoke();
        }

        public void CreateRoom(string roomName, RoomOptions roomOptions)
        {
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        public override void OnCreatedRoom()
        {
            FixedDebug.Log($"Created room: {PhotonNetwork.CurrentRoom.Name}");

            eventOnCreatedRoom.Invoke();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            FixedDebug.Log($"Create room failed: ({returnCode}) {message}");

            eventOnCreateRoomFailed.Invoke(returnCode);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            FixedDebug.Log($"Room list update.");

            eventOnRoomListUpdate.Invoke(roomList);
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            FixedDebug.Log("Room Properties Update.");

            eventOnRoomPropertiesUpdate.Invoke(propertiesThatChanged);
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        public void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public void JoinRandomOrCreateRoom()
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }
}