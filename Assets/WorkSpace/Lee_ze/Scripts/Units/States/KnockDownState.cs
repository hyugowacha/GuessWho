using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownState : IPlayerStates
{
    PlayerControl player;

    public void EnterState(PlayerControl player)
    {
        player.moveAnimation.SetBool("IsKnockDown", true);
    }

    public void UpdatePerState()
    {
        //AnimatorStateInfo stateInfo = player.moveAnimation.GetCurrentAnimatorStateInfo(0);

        //if (stateInfo.IsName("KnockDown") && stateInfo.normalizedTime >= 1.0f)
        //{
        //    player.moveAnimation.SetBool("IsKnockDown", false);
        //}
    }

    public void ExitState()
    {

    }
}
