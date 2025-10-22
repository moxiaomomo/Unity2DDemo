using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestPlayerIdleState : PlayerState
{
    public ForestPlayerIdleState(PlayerBase _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!player.isBusy && (yInput!=0||xInput!=0))
        {
            stateMachine.ChangeState(player.moveState, "Move");
        }
    }
}
