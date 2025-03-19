using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerStates
{
    Player player;

    public void EnterState(Player player)
    {
        this.player = player;
    }

    public void UpdatePerState()
    {
        player.moveSpeed = Mathf.Lerp(player.moveSpeed, 0, Time.deltaTime * 5f);

        player.moveAnimation.SetFloat("Speed", player.moveSpeed / 0.12f);

        if (player.direction != Vector2.zero)
        {
            player.ChangeStateTo(new MoveState());
        }
    }

    public void ExitState()
    {

    }
}
