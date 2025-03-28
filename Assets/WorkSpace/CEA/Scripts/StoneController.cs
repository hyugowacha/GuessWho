using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StoneController : MonoBehaviour
{
    PlayerControl player;
    TestingNPC npc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerControl>();
            player.GetHit();
            Destroy(this.gameObject);
        }

        else if (other.CompareTag("NPC"))
        {
            npc = other.GetComponent<TestingNPC>();   
        }
    }
}
