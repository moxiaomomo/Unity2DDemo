using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestPlayerMoveState : PlayerState
{
    private float stopX, stopY;

    public ForestPlayerMoveState(PlayerBase _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
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

        // 注意顺序:先改变state,再更新velocity
        if (xInput == 0 && yInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else
        {
            stopX = xInput;
            stopY = yInput;
        }

        Vector3 vel = (player.transform.right * xInput + player.transform.up * yInput).normalized;
        if (!player.CanMove())
        {
            player.SetZeroVelocity();
        }
        else
        {
            player.SetVelocity(vel.x * player.moveSpeed, vel.y * player.moveSpeed, false);
        }
        stateMachine.currentState.SetFloatParam("InputX", stopX);
        stateMachine.currentState.SetFloatParam("InputY", stopY);
    }
}
