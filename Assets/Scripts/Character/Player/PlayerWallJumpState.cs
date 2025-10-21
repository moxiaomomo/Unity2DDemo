using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
        player.SetVelocity(player.moveSpeed * 0.8f * -player.facingDirection, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(((Player)player).wallState);
        }
        if (rb.velocity.y<0)
        {
            stateMachine.ChangeState(((Player)player).airState);
        }
    }
}
