using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownState : IPlayerStates
{
    PlayerControl player;

    public void EnterState(PlayerControl player)
    {
        this.player = player;

        this.player.audioSource.PlayOneShot(player.getHit, UnityEngine.Random.Range(0.5f, 1f)); // 피격 사운드

        player.playerAnim.SetBool("IsKnockDown", true);
    }

    public void UpdatePerState()
    {
        //Any State에서 IsKnockDown이 반복적으로 true가 되는 현상을 없앰.
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
