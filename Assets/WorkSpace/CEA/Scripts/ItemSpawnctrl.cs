using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnctrl : MonoBehaviour
{
    [SerializeField]
    private GameObject gunItem;

    [SerializeField]
    private GameObject whistleItem;

    [SerializeField]
    private GameObject stoneItem;

    //자식 객체가 모두 꺼져있는지 확인하는 메서드
    public bool AllItemDisabled(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    } 

    public void ItemSpawn() //아이템을 스폰하는 함수
    {
        int itemNum = Random.Range(1, 11);

        switch (itemNum)
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
