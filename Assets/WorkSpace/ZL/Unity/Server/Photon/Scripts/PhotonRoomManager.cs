using Photon.Pun;

using Photon.Realtime;

using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Room Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PhotonRoomManager : MonoBehaviourPunCallbacks, ISingleton<PhotonRoomManager>
    {
        [Space]

        [SerializeField]

        private UnityEvent eventOnJoinedRoom;

        [Space]

        [SerializeField]

        private UnityEvent<short> evenOnJoinRoomFailed;

        [Space]

        [SerializeField]

        private UnityEvent eventOnLeftRoom;

        [Space]

        [SerializeField]

        private UnityEvent eventOnMasterClientLeftRoom;

        [Space]

        [SerializeField]

        private UnityEvent eventOnMasterClientSwitched;

        private void Awake()
        {
            ISingleton<PhotonRoomManager>.TrySetInstance(this);
        }

        private void OnDestroy()
        {
            ISingleton<PhotonRoomManager>.Release(this);
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
            FixedDebug.LogWarning($"Join random failed: ({returnCode}) {message}");

            evenOnJoinRoomFailed.Invoke(returnCode);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            FixedDebug.Log($"Left room.");

            if (PhotonNetwork.IsMasterClient == true)
            {
                OnMasterClientLeftRoom();
            }

            eventOnLeftRoom.Invoke();
        }

        public void OnMasterClientLeftRoom()
        {
            ISingleton<PhotonManager>.Instance.RPCLog(RpcTarget.All, "Master client left room.");

            eventOnMasterClientLeftRoom.Invoke();
        }

        public void SetMasterClientInOrder()
        {
            var playerList = PhotonNetwork.PlayerList;

            foreach (var player in playerList)
            {
                if (player.IsMasterClient == false)
                {
                    PhotonNetwork.SetMasterClient(player);
                }
            }
        }

        public void SetMasterClientRandom()
        {
            var playerList = PhotonNetwork.PlayerList;

            if (playerList.Length != 0)
            {
                var newMasterClient = playerList[Random.Range(0, playerList.Length)];

                PhotonNetwork.SetMasterClient(newMasterClient);
            }
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            FixedDebug.Log($"Master client switched: {newMasterClient.NickName}");

            eventOnMasterClientSwitched.Invoke();
        }
    }
}