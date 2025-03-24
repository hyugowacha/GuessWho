using Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField]
    [Header("아이템 스폰 포인트")] private GameObject[] spawnPoints;

    [SerializeField]
    [Header("포톤뷰")] private PhotonView photonView;
    
    private ItemSpawnctrl itemSpawnctrl;


    //모든 포인트를 총괄하는 itemspawnpoints 오브젝트가 처음 실행되어서 할 메서드
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(PhotonNetwork.InRoom)
            {
                int itemNum = Random.Range(1, 11);
                photonView.RPC("RPC_SpawnItems", RpcTarget.All, itemNum);
            }
        }
    }

    [PunRPC]
    private void RPC_SpawnItems(int itemNum)
    {
        foreach(var spawnPoint in spawnPoints)
        {
            itemSpawnctrl = spawnPoint.GetComponent<ItemSpawnctrl>();

            if (itemSpawnctrl != null)
            {
                itemSpawnctrl.ItemSpawn(itemNum);
            }
        }
    }


}
