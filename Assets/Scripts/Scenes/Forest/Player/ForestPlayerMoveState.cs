using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestPlayerMoveState : PlayerState
{
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
        else if (!player.isBusy)
        {
            if (yInput > 0 && stateMachine.NeedChangeState(player.moveState, "MoveBack"))
            {
                stateMachine.ChangeState(player.moveState, "MoveBack");
            }
            else if (yInput < 0 && stateMachine.NeedChangeState(player.moveState, "MoveFront"))
            {
                stateMachine.ChangeState(player.moveState, "MoveFront");
            }
            else if (xInput != 0 && stateMachine.NeedChangeState(player.moveState, "MoveHorizontal"))
            {
                stateMachine.ChangeState(player.moveState, "MoveHorizontal");
            }
        }

        if (!player.CanMove())
        {
            player.SetZeroVelocity();
        }
        else
        {
            player.SetVelocity(xInput * player.moveSpeed, yInput * player.moveSpeed);
        }
    }
}
