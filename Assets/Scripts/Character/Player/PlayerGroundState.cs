using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && player.counterAttackCoolTimeCounter<0)
        {
            player.counterAttackCoolTimeCounter = player.counterAttackCoolTime;
            stateMachine.ChangeState(player.counterAttackState);
        }

        //Ó°ÏìÌøµÍÅÀÇ½
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
