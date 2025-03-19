using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField]
    [Header("아이템 스폰 포인트")] private GameObject[] spawnPoints;

    
    private void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            ItemSpawn(spawnPoint);
        }
    }


    void Update()
    {
        
    }

    void ItemSpawn(GameObject spawnPoint)
    {
        int itemNum = Random.Range(1, 11);
        GameObject gunItem = spawnPoint.transform.Find("GunItem")?.gameObject;
        GameObject whistleItem = spawnPoint.transform.Find("WhistleItem")?.gameObject;
        GameObject stoneItem = spawnPoint.transform.Find("StoneItem")?.gameObject;
     

        switch(itemNum)
        {
            case 1:
                gunItem.SetActive(true); 
                break;

            case 2:
                whistleItem.SetActive(true);
                break;

            default:
                stoneItem.SetActive(true);
                break;
        }
    }
}
