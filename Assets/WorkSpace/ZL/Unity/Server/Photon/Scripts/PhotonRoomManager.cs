using Photon.Pun;

using Photon.Realtime;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Collections;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Room Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PhotonRoomManager :
        
        MonoBehaviourPunCallbacks, ISingleton<PhotonRoomManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        private string currentRoomName = string.Empty;

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

        private UnityEvent eventOnCreatedRoom;

        [Space]

        [SerializeField]

        private UnityEvent eventOnCreateRoomFailed;

        [Space]

        [SerializeField]

        private UnityEvent eventOnJoinedRoom;

        [Space]

        [SerializeField]

        private UnityEvent evenOnJoinRoomFailed;
        
        [Space]

        [SerializeField]

        private UnityEvent eventOnLeftRoom;

        private void Awake()
        {
            ISingleton<PhotonRoomManager>.TrySetInstance(this);
        }

        private void OnDestroy()
        {
            ISingleton<PhotonRoomManager>.Release(this);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            RoomList = roomList;
        }

        public void CreateRoom(string roomName)
        {
            currentRoomName = roomName;

            PhotonNetwork.CreateRoom(roomName, null);
        }

        public void CreateRoom(string roomName, RoomOptions roomOptions)
        {
            currentRoomName = roomName;

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        public override void OnCreatedRoom()
        {
            FixedDebug.Log($"Created Room: {currentRoomName}");

            eventOnCreatedRoom.Invoke();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            FixedDebug.Log($"Create Room Failed ({returnCode}): {message}");

            eventOnCreateRoomFailed.Invoke();
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

        public override void OnJoinedRoom()
        {
            currentRoomName = PhotonNetwork.CurrentRoom.Name;

            FixedDebug.Log($"Joined Room: {currentRoomName}");

            eventOnJoinedRoom.Invoke();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            FixedDebug.Log($"Join Room Failed ({returnCode}): {message}");

            evenOnJoinRoomFailed.Invoke();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            FixedDebug.Log($"Join Random Failed ({returnCode}): {message}");

            evenOnJoinRoomFailed.Invoke();
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            FixedDebug.Log($"Left Room: {currentRoomName}");

            eventOnLeftRoom.Invoke();
        }
    }
}