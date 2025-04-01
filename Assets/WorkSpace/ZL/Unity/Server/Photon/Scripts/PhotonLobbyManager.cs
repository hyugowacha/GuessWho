using Photon.Pun;

using Photon.Realtime;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Collections;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Lobby Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PhotonLobbyManager :
        
        MonoBehaviourPunCallbacks, ISingleton<PhotonLobbyManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private int minPlayerCount = 2;

        public int MinPlayerCount => minPlayerCount;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private int maxPlayerCount = 2;

        public int MaxPlayerCount => maxPlayerCount;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        private Wrapper<List<RoomInfo>> roomList;

        public List<RoomInfo> RoomList
        {
            get => roomList.value;

            private set => roomList.value = value;
        }

        [Space]

        [SerializeField]

        private UnityEvent eventOnJoinedLobby;

        [Space]

        [SerializeField]

        private UnityEvent evenOnLeftLobby;

        [Space]

        [SerializeField]

        private UnityEvent eventOnCreatedRoom;

        [Space]

        [SerializeField]

        private UnityEvent<short> eventOnCreateRoomFailed;

        [Space]

        [SerializeField]

        private UnityEvent eventOnRoomListUpdate;

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

        public void CreateRoom()
        {
            CreateRoom(null, null);
        }

        public void CreateRoom(RoomOptions roomOptions)
        {
            CreateRoom(null, roomOptions);
        }

        public void CreateRoom(string roomName)
        {
            CreateRoom(roomName, null);
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

            RoomList = roomList;

            eventOnRoomListUpdate.Invoke();
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