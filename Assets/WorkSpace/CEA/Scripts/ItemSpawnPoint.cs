using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField]
    [Header("아이템 스폰 포인트")] private GameObject[] spawnPoints;
    
    private ItemSpawnctrl itemSpawnctrl;
    private float respawnCoolTime = 20.0f;
    private float respawnElapsedTime;

    private void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            itemSpawnctrl = spawnPoint.GetComponent<ItemSpawnctrl>();

            if (itemSpawnctrl != null)
            {
                itemSpawnctrl.ItemSpawn();
            }
        }
    }


    void Update()
    {
        if (!itemSpawnctrl.AllItemDisabled(itemSpawnctrl.transform))
        {
            return;
        }

        else
        {
            respawnElapsedTime += Time.deltaTime;
            Debug.Log("아이템 리스폰 대기중: " + respawnElapsedTime);
            if(respawnElapsedTime > respawnCoolTime)
            {
                itemSpawnctrl.ItemSpawn();
                respawnElapsedTime = 0;
            }
        }
    }

    
}
