using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackState : IPlayerStates
{
    PlayerControl player;

    ItemType itemType;

    public void EnterState(PlayerControl player)
    {
        // TOOD: PlayerControl에서 무기 바꾸는 로직. 여기서 하는거 아님

        this.player = player;

        if(player.holdingWeapon != null)
        {
            itemType = player.holdingWeapon.itemType;
        }
        
        if(player.holdingWeapon == null)
        {
            itemType = player.footData.itemType;
        }

        player.moveSpeed = 0;

        // 공격 로직
        switch (itemType)
        {
            case (ItemType.None):

                player.StartCoroutine(AttackKick());

                break;

            case (ItemType.Stone):

                AttackThrow();
                Debug.Log("돌 던지기");
                break;

            case (ItemType.Gun):

                AttackShoot();
                Debug.Log("총쏘기");
                break;
        }
    }

    public void UpdatePerState()
    {
        if (player.isAttackTriggered == false)
        {// ※ 추가되는 다른 공격 마지막에 player.isAttackTriggered = false;
            player.ChangeStateTo(new IdleState());

            return;
        }

        if (player.isHit == true)
        {
            player.ChangeStateTo(new KnockDownState());

            return;
        }
    }

    public void ExitState()
    {

    }

    IEnumerator AttackKick()
    {
        player.playerAnim.SetBool("IsKick", true);

        if (player.photonView.IsMine)
        {
            player.audioSource.PlayOneShot(player.kick); // 로컬 kick 사운드

            player.photonView.RPC("RPC_PlayAttackSound", RpcTarget.Others, player.transform.position); // RPC로 kick 사운드 나게 함
        }

        yield return new WaitForSeconds(1f);

        player.playerAnim.SetBool("IsKick", false);

        player.isAttackTriggered = false;
    }

    void AttackThrow()
    {

        player.isAttackTriggered = false;
    }

    void AttackShoot()
    {
        Debug.Log("총 쏘기");
        player.isAttackTriggered = false;
    }
}
