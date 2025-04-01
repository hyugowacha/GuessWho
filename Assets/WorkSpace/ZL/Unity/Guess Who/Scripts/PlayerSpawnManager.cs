using Photon.Pun;

using UnityEngine;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Player Spawn Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PlayerSpawnManager :
        
        MonoBehaviour, ISingleton<PlayerSpawnManager>
    {
        [Space]

        [SerializeField]

        private GameObject playerPrefab;

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

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, position, rotation);
        }

        public void SpawnRandom()
        {
            int randomPoint = Random.Range(0, playerSpawnPoints.Length);

            Spawn(playerSpawnPoints[randomPoint].position, Quaternion.identity);
        }
    }
}