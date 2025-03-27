using Photon.Pun;

using Photon.Realtime;

using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Player Manager (Singleton)")]

    public class PhotonPlayerManager : PhotonPlayerManager<PhotonPlayerManager> { }

    [DisallowMultipleComponent]

    public abstract class PhotonPlayerManager<T> :
        
        MonoBehaviourPunCallbacks, ISingleton<T>

        where T : PhotonPlayerManager<T>
    {
        [Space]

        [SerializeField]

        private GameObject playerPrefab;

        [Space]

        [SerializeField]

        private UnityEvent eventOnPlayerEnteredRoom;

        [Space]

        [SerializeField]

        private UnityEvent eventOnPlayerLeftRoom;

        [Space]

        [SerializeField]

        private UnityEvent eventOnPlayerListUpdated;

        [Space]

        [SerializeField]

        private Transform[] playerSpawnPoint;

        private void Awake()
        {
            ISingleton<T>.TrySetInstance((T)this);
        }

        private void OnDestroy()
        {
            ISingleton<T>.Release((T)this);
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

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, position, rotation);
        }

        public void SpawnRandom()
        {
            int randomPoint = Random.Range(0, playerSpawnPoint.Length);

            Spawn(playerSpawnPoint[randomPoint].position, Quaternion.identity);
        }
    }
}