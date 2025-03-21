using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApologizeState : IPlayerStates
{
    PlayerControl player;

    public void EnterState(PlayerControl player)
    {
        this.player = player;
    }

    public void UpdatePerState()
    {
        player.moveSpeed = 0;

        player.StartCoroutine(Apologize());

        if (player.isNPC == false)
        {
            player.ChangeStateTo(new IdleState());
        }
    }

    public void ExitState()
    {
        
    }

    IEnumerator Apologize()
    {
        player.playerAnim.SetBool("IsNPC", true);

        yield return new WaitForSeconds(4f);

        player.playerAnim.SetBool("Forgived", true);

        yield return null;

        player.isNPC = false;
    }
}