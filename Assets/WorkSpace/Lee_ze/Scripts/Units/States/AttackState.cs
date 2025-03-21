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
        this.player = player;

        itemType = player.holdingWeapon;

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
        if (player.isAttackTriggered == false) // ※ 공격 마지막에 player.isAttackTriggered = false;
        {
            player.ChangeStateTo(new IdleState());
        }
    }

    public void ExitState()
    {

    }

    IEnumerator AttackKick()
    {
        player.kickAnimation.SetBool("IsKick", true);

        yield return new WaitForSeconds(1f);

        player.kickAnimation.SetBool("IsKick", false);

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
