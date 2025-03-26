using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownState : IPlayerStates
{
    PlayerControl player;

    public void EnterState(PlayerControl player)
    {
        this.player = player;

        player.playerAnim.SetBool("IsKnockDown", true);
    }

    public void UpdatePerState()
    {
        AnimatorStateInfo stateInfo = player.playerAnim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("KnockDown") == false)
        {
            return;
        }

        player.playerAnim.SetBool("IsKnockDown", false);
    }

    public void ExitState()
    {

    }
}
