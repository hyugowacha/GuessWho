using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using ZL.Unity;

public class PlayerUI : MonoBehaviour
{
    public Image normalAttackSlot;
    public Image WeaponAttackSlot;

    public Image normalSlotSelect;
    public Image WeaponSlotSelect;

    public GameObject leftBullet;
    public Image[] weaponImages;

    TextMeshPro leftBulletText;


    private void Awake()
    {
        leftBulletText = leftBullet.GetComponent<TextMeshPro>();
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => PhotonNetwork.InRoom);
        normalAttackSlot.SetActive(true);
        WeaponAttackSlot.SetActive(true);
    }

    public void ChangeWeaponSelectUI(PlayerControl player)
    {
        if (player.holdingWeapon == player.footData || player.holdingWeapon == null)
        {
            if (!normalSlotSelect.enabled)
            {
                normalSlotSelect.SetActive(true);
                WeaponSlotSelect.SetActive(false);
            }
        }

        else if (player.holdingWeapon.itemType == ItemType.Stone || 
            player.holdingWeapon.itemType == ItemType.Gun)
        {
            if (!WeaponSlotSelect.enabled)
            {
                WeaponSlotSelect.SetActive(true);
                normalSlotSelect.SetActive(false);
            }
        }
    }
}
