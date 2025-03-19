using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerStates
{
    StateManager player;

    Vector2 direction;

    public void EnterState(StateManager player)
    {
        this.player = player;

        direction = player.direction;
    }

    public void UpdatePerState()
    {
        direction = player.direction;

        if (direction != Vector2.zero)
        {
            player.ChangeStateTo(new MoveState());
        }
    }

    public void ExitState()
    {

    }
}
