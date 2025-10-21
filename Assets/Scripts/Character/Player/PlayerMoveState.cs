using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(PlayerBase _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
    {       
    }

    public override void Enter()
    {
        base.Enter();
        // AudioManager.instance.PlaySFX(5);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(!player.CanMove())
        {
            player.SetZeroVelocity();
        }
        else
        {
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        }
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
