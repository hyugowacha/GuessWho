using Photon.Pun;

using Photon.Realtime;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Pooling;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Player List (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PhotonPlayerList :
        
        MonoBehaviourPunCallbacks, ISingleton<PhotonPlayerList>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private ManagedObjectPool<PhotonPlayerListItem> itemPool;

        [Space]

        [SerializeField]

        private UnityEvent eventOnPlayerEnteredRoom;

        [Space]

        [SerializeField]

        private UnityEvent eventOnPlayerLeftRoom;

        [Space]

        [SerializeField]

        private UnityEvent eventOnPlayerListUpdated;

        private void Awake()
        {
            ISingleton<PhotonPlayerList>.TrySetInstance(this);
        }

        private void OnDestroy()
        {
            ISingleton<PhotonPlayerList>.Release(this);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            FixedDebug.Log($"Player entered room: {newPlayer.NickName}");

            eventOnPlayerEnteredRoom.Invoke();

            OnPlayerListUpdate();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            FixedDebug.Log($"Player left room: {otherPlayer.NickName}");

            eventOnPlayerLeftRoom.Invoke();

            OnPlayerListUpdate();
        }

        public void OnPlayerListUpdate()
        {
            FixedDebug.Log("Player List Update.");

            eventOnPlayerListUpdated.Invoke();
        }

        public void Refresh()
        {
            if (itemPool.Original != null)
            {
                itemPool.Recall();

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    var item = itemPool.Generate();

                    item.Initialize(player.NickName);

                    item.SetActive(true);
                }
            }
        }
    }
}