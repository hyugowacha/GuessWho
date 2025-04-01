using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeStateAfterHit : MonoBehaviour
{
    
    [SerializeField] TestingNPC npcScript;
    public void ChangeAfterHit()
    {

            var nowState = npcScript.NowState;
            npcScript.AfterHit();
  
            npcScript.SelfCollider.enabled = true;

    }
}
