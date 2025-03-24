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
            Vector3 position = new Vector3(0f, 1f, 0f);

            PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        }
    }
}