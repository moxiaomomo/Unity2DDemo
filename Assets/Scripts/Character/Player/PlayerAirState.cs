using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed * .8f, rb.velocity.y);

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(((Player)player).wallState);
        }
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
