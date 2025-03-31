using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StoneController : MonoBehaviour
{
    PlayerControl player;
    TestingNPC npc;

    float maxLifetime = 4.0f;
    float elapsedLifetime = 0;

    [SerializeField]
    private GameObject stoneDestroyEffect;

    private void Update()
    {
        elapsedLifetime += Time.deltaTime;

        if(elapsedLifetime > maxLifetime)
        {
            Destroy(this.gameObject);
            elapsedLifetime = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerControl>();
            player.GetHit();
            Destroy(this.gameObject);
            Instantiate(stoneDestroyEffect, this.transform);
        }

        else if (other.CompareTag("NPC"))
        {
            npc = other.GetComponent<TestingNPC>();
            Destroy(this.gameObject);
            Instantiate(stoneDestroyEffect, this.transform);
        }

        else if (other.CompareTag("Map"))
        {
            Destroy(this.gameObject);
            Instantiate(stoneDestroyEffect, this.transform);
        }
    }
}
