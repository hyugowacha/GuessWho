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
        
            
            var nowState = npcScript.NowState;
            npcScript.AfterHit();
            
            //ChangeState(NPCStateName.Idle);//new NPCIdle());
            npcScript.SelfCollider.enabled = true;
        
        //hitTime = 0;
    }
}
