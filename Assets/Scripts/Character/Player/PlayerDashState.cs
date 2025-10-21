using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerBase _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if(!player.IsGroundDetected()&&player.IsWallDetected())
        {
            stateMachine.ChangeState(((Player)player).wallState);
        }

        player.SetVelocity(player.dashDiretion * (player.moveSpeed + player.dashSpeed), 0);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
