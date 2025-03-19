using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IPlayerStates
{
    Player player;

    public void EnterState(Player player)
    {
        this.player = player;
    }

    public void UpdatePerState()
    {
        Vector3 camForward = Camera.main.transform.forward;

        camForward.y = 0;

        Vector3 camRight = Camera.main.transform.right;

        camRight.y = 0;

        if (player.direction != Vector2.zero)
        {
            player.targetSpeed = player.isRunning ? 0.12f : 0.06f;

            player.moveSpeed = Mathf.Lerp(player.moveSpeed, player.targetSpeed, Time.deltaTime * 5f);

            Vector3 moveDir = (camRight * player.direction.x + camForward * player.direction.y).normalized;

            Vector3 move = player.moveSpeed * new Vector3(moveDir.x, 0, moveDir.z);

            player.rb.MovePosition(player.transform.position + move);

            player.rb.MovePosition(player.transform.position + move);

            player.modelRotator.SetTargetDirection(move);
        }
        else
        {
            player.moveSpeed = Mathf.Lerp(player.moveSpeed, 0, Time.deltaTime * 5f);
        }

        player.moveAnimation.SetFloat("Speed", player.moveSpeed / 0.12f);

        // 움직임이 없을 때 IdleState로 전환
        if (player.direction == Vector2.zero)
        {
            player.ChangeStateTo(new IdleState());
        }

        if (player.isAttackTriggered == true)
        {
            player.ChangeStateTo(new AttackState());
        }
    }

    public void ExitState()
    {

    }
}
