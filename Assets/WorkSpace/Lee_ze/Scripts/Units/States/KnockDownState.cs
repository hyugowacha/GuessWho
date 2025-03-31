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

        this.player.audioSource.PlayOneShot(player.getHit); // 로컬 피격 사운드

        this.player.photonView.RPC("RPC_PlayHitSound", RpcTarget.Others, player.transform.position); // RPC로 kick 사운드 나게 함

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
