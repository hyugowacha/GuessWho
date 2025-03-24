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
            PhotonNetwork.InstantiateRoomObject("ItemSpawnPoint", Vector3.zero,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
