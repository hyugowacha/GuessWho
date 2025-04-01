using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeStateAfterHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TestingNPC npcScript;
    public void ChangeAfterHit()
    {
        
            Debug.Log("hitend");
            var nowState = npcScript.NowState;
            npcScript.AfterHit();
            Debug.Log("3");
            //ChangeState(NPCStateName.Idle);//new NPCIdle());
            npcScript.SelfCollider.enabled = true;
        
        //hitTime = 0;
    }
}
