using Photon.Pun;

using Photon.Realtime;

using System;

using System.Collections;

using System.Collections.Generic;

using System.Diagnostics;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Collections;

using ZL.Unity.IO;

using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Server Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PhotonServerManager
        
        : MonoBehaviourPunCallbacks, ISingleton<PhotonServerManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private StringPref nicknamePref = new("Nickname", string.Empty);

        public string Nickname => nicknamePref.Value;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private Wrapper<TypedLobby[]> lobbies;

        [Space]

        [SerializeField]

        private float fakeLoadingTime = 0f;

        [Space]

        [SerializeField]

        private UnityEvent eventOnConnectingToMaster;

        public UnityEvent EventOnConnectingToMaster => eventOnConnectedToMaster;

        [Space]

        [SerializeField]

        private UnityEvent eventOnConnectedToMaster;

        public UnityEvent EventOnConnectedToMaster => eventOnConnectedToMaster;

        [Space]

        [SerializeField]

        private UnityEvent eventOnDisconnected;

        public UnityEvent EventOnDisconnected => eventOnConnectedToMaster;

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

        private UnityEvent<List<RoomInfo>> eventOnRoomListUpdate;

        public UnityEvent<List<RoomInfo>> EventOnRoomListUpdate => eventOnRoomListUpdate;

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

        private UnityEvent<Hashtable> eventOnRoomPropertiesUpdate;

        public UnityEvent<Hashtable> EventOnRoomPropertiesUpdate => eventOnRoomPropertiesUpdate;

        [Space]

        [SerializeField]

        private UnityEvent eventOnJoinedRoom;

        public UnityEvent EventOnJoinedRoom => eventOnJoinedRoom;

        [Space]

        [SerializeField]

        private UnityEvent<short> evenOnJoinRoomFailed;

        public UnityEvent<short> EvenOnJoinRoomFailed => evenOnJoinRoomFailed;

        [Space]

        [SerializeField]

        private UnityEvent eventOnLeftRoom;

        public UnityEvent EventOnLeftRoom => eventOnLeftRoom;

        [Space]

        [SerializeField]

        private UnityEvent<Player> eventOnPlayerEnteredRoom;

        public UnityEvent<Player> EventOnPlayerEnteredRoom => eventOnPlayerEnteredRoom;

        [Space]

        [SerializeField]

        private UnityEvent<Player> eventOnPlayerLeftRoom;

        public UnityEvent<Player> EventOnPlayerLeftRoom => eventOnPlayerLeftRoom;

        [Space]

        [SerializeField]

        private UnityEvent eventOnMasterClientSwitched;

        public UnityEvent EventOnMasterClientSwitched => eventOnMasterClientSwitched;

        private Dictionary<string, TypedLobby> lobbyDictionary;

        private readonly Stopwatch loadingStopwatch = new();

        private void Awake()
        {
            ISingleton<PhotonServerManager>.TrySetInstance(this);

            nicknamePref.ActionOnValueChanged += (value) =>
            {
                PhotonNetwork.NickName = value;
            };

            nicknamePref.TryLoadValue();

            if (lobbies.value.Length != 0)
            {
                lobbyDictionary = new(lobbies.value.Length);

                foreach (var lobby in lobbies.value)
                {
                    lobbyDictionary.Add(lobby.Name, lobby);
                }
            }
        }

        private void OnDestroy()
        {
            ISingleton<PhotonServerManager>.Release(this);
        }

        public void ConnectToMaster()
        {
            OnConnectingToMaster();

            loadingStopwatch.Restart();

            if (PhotonNetwork.IsConnected == false)
            {
                PhotonNetwork.GameVersion = Application.version;

                PhotonNetwork.ConnectUsingSettings();
            }

            else
            {
                OnConnectedToMaster();
            }
        }

        public void OnConnectingToMaster()
        {
            FixedDebug.Log("Connecting To Master...");

            eventOnConnectingToMaster.Invoke();
        }

        public override void OnConnectedToMaster()
        {
            loadingStopwatch.Stop();

            float loadingTime = (float)loadingStopwatch.Elapsed.TotalSeconds;

            StartCoroutine(FakeLoading(loadingTime, () =>
            {
                FixedDebug.Log($"Connected To Master. (Version: {PhotonNetwork.GameVersion})");

                eventOnConnectedToMaster.Invoke();
            }));
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            loadingStopwatch.Stop();

            float loadingTime = (float)loadingStopwatch.Elapsed.TotalSeconds;

            StartCoroutine(FakeLoading(loadingTime, () =>
            {
                FixedDebug.LogWarning($"Disconnected: {cause}");

                eventOnDisconnected.Invoke();
            }));
        }

        public bool TrySetNickname(string nickname, out NicknameValidationException exception)
        {
            if (nickname.IsValidNickname(out exception) == false)
            {
                FixedDebug.LogWarning($"Try Set Nickname failed: {exception}");

                return false;
            }

            nicknamePref.SaveValue(nickname);

            return true;
        }

        public void JoinLobby()
        {
            PhotonNetwork.JoinLobby();
        }

        public void JoinLobby(string name)
        {
            PhotonNetwork.JoinLobby(lobbyDictionary[name]);
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

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            FixedDebug.Log($"Room list update.");

            eventOnRoomListUpdate.Invoke(roomList);
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

        public override void OnJoinedRoom()
        {
            FixedDebug.Log($"Joined room: {PhotonNetwork.CurrentRoom.Name}");

            eventOnJoinedRoom.Invoke();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            FixedDebug.Log($"Join room failed: ({returnCode}) {message}");

            evenOnJoinRoomFailed.Invoke(returnCode);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            FixedDebug.Log($"Join random failed: ({returnCode}) {message}");

            evenOnJoinRoomFailed.Invoke(returnCode);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            FixedDebug.Log($"Left room.");

            eventOnLeftRoom.Invoke();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            FixedDebug.Log($"Player {newPlayer.ActorNumber} '{newPlayer.NickName}' entered room.");

            eventOnPlayerEnteredRoom.Invoke(newPlayer);

            if (PhotonNetwork.IsMasterClient == true)
            {
                var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;

                PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            FixedDebug.Log($"Player {otherPlayer.ActorNumber} '{otherPlayer.NickName}' left room.");

            eventOnPlayerLeftRoom.Invoke(otherPlayer);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            FixedDebug.Log($"Master client switched to Player {newMasterClient.ActorNumber} '{newMasterClient.NickName}'");

            eventOnMasterClientSwitched.Invoke();
        }

        public void SetRoomOpened(bool value)
        {
            PhotonNetwork.CurrentRoom.IsOpen = value;
        }

        private IEnumerator FakeLoading(float loadingTime, Action callback)
        {
            loadingTime = fakeLoadingTime - loadingTime;

            if (loadingTime > 0f)
            {
                yield return WaitFor.Seconds(loadingTime);
            }

            callback.Invoke();
        }
    }
}