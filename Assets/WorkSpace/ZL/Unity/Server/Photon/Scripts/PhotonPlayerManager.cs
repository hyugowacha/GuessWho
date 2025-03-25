using UnityEngine;

using Photon.Pun;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Player Manager (Singleton)")]

    public class PhotonPlayerManager : PhotonPlayerManager<PhotonPlayerManager> { }

    [DisallowMultipleComponent]

    public abstract class PhotonPlayerManager<T> : MonoBehaviour, ISingleton<T>

        where T : PhotonPlayerManager<T>
    {
        [Space]

        [SerializeField]

        private GameObject playerPrefab;

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

        public void Instantiate()
        {
            int randomPoint = Random.Range(0, playerSpawnPoint.Length);

            PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoint[randomPoint].position, Quaternion.identity);
        }
    }
}