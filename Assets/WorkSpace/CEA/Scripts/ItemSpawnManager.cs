using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class ItemSpawnManager : MonoBehaviourPunCallbacks
{
    IEnumerator Start()
    {
        yield return new WaitUntil(() => PhotonNetwork.InRoom);
        Debug.Log("방에 들어왔음");

        yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("아이템 스폰됐음");
            PhotonNetwork.Instantiate("ItemSpawnPoints", Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("ItemSpawnPoints", Vector3.zero, Quaternion.identity);
        }
    }

}
