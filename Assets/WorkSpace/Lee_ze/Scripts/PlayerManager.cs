using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public Transform[] playerSpawnPoint;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            int randomPoint = Random.Range(0, playerSpawnPoint.Length);

            // 플레이어 생성
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoint[randomPoint].position, Quaternion.identity);
        }
    }
}
