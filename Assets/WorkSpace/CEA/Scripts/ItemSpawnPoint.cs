using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField]
    [Header("아이템 스폰 포인트")] private GameObject[] spawnPoints;
    
    private ItemSpawnctrl itemSpawnctrl;
    

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

    
}
