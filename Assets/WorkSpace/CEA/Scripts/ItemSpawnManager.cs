using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class ItemSpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject ItemSpawnPoint;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject("ItemSpawnPoints", Vector3.zero, Quaternion.identity);
        }
    }

}
