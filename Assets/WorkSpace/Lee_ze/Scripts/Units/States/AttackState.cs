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

        itemType = player.holdingWeapon.itemType;

        player.moveSpeed = 0;

        // 공격 로직
        switch (itemType)
        {
            case (ItemType.None):

                player.StartCoroutine(AttackKick());

                break;

            case (ItemType.Stone):

                AttackThrow();

                break;

            case (ItemType.Gun):

                AttackShoot();

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

        yield return new WaitForSeconds(1f);

        player.playerAnim.SetBool("IsKick", false);

        player.isAttackTriggered = false;
    }

    void AttackThrow()
    {
        Debug.Log("돌 던지기");
    }

    void AttackShoot()
    {
        Debug.Log("총 쏘기");
    }
}
