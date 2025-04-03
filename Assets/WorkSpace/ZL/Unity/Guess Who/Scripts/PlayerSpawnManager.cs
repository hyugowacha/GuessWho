using Photon.Pun;

using UnityEngine;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Player Spawn Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PlayerSpawnManager
        
        : MonoBehaviour, ISingleton<PlayerSpawnManager>
    {
        [Space]

        [SerializeField]

        private GameObject playerPrefab;

        [SerializeField]

        private Transform parent;

        [Space]

        [SerializeField]

        private Transform[] playerSpawnPoints;

        private void Awake()
        {
            ISingleton<PlayerSpawnManager>.TrySetInstance(this);
        }

        private void OnDestroy()
        {
            ISingleton<PlayerSpawnManager>.Release(this);
        }

        public void SpawnRandom()
        {
            int index = Random.Range(0, playerSpawnPoints.Length);

            Spawn(playerSpawnPoints[index].position, Quaternion.identity);
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            var player = PhotonNetwork.Instantiate(playerPrefab.name, position, rotation);

            player.transform.SetParent(parent, true);
        }
    }
}