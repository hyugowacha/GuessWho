using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sd : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //Debug.Log("master changed");
        //base.OnMasterClientSwitched(newMasterClient);
        if (PhotonNetwork.IsMasterClient)
        {
            //Debug.Log("itsmaster");
            //SelfAgent.enabled = true;
            //StartCoroutine(CheckState());
        }




    }
}
    
