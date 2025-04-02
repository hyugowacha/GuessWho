using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Photon.Pun;

public class StoneController : MonoBehaviour
{
    PlayerControl player;
    TestingNPC npc;

    float maxLifetime = 4.0f;
    float elapsedLifetime = 0;

    bool canCreateSoundPlayer = true;

    public int whoThrow;
    public GameObject stoneSoundPlayer;

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
            PhotonView photonView = other.GetComponent<PhotonView>();

            if (photonView.ViewID != whoThrow)
            {
                player = other.GetComponent<PlayerControl>();
                player.GetHit();

                if (canCreateSoundPlayer)
                {
                    Instantiate(stoneSoundPlayer, transform.position, Quaternion.identity);
                    canCreateSoundPlayer = false;
                }
                

                Instantiate(stoneDestroyEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

        else if (other.CompareTag("NPC"))
        {
            npc = other.GetComponent<TestingNPC>();
            Instantiate(stoneSoundPlayer, transform.position, Quaternion.identity);

            Instantiate(stoneDestroyEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        else if (other.CompareTag("Map"))
        {
            if (canCreateSoundPlayer)
            {
                Instantiate(stoneSoundPlayer, transform.position, Quaternion.identity);
                canCreateSoundPlayer = false;
            }

            Instantiate(stoneDestroyEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
