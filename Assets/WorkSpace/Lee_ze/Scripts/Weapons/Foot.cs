using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField]
    private PlayerControl player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IHittable>()?.GetHit();

            player.isNPC = false;
        }

        if (other.CompareTag("NPC"))
        {
            other.GetComponent<IHittable>()?.GetHit();

            player.isNPC = true;
        }
    }
}
