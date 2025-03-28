using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StoneController : MonoBehaviour
{
    PlayerControl player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetHit();
            Destroy(this.gameObject);
        }

        else if (other.CompareTag("NPC"))
        {
            
        }
    }
}
