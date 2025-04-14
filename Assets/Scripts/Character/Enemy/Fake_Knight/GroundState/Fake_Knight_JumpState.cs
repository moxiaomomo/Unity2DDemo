using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_JumpState : Fake_Knight_GroundState
{

    public Fake_Knight_JumpState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .5f;
        // 初始化boss下落不进行加速度
        if (boss.lastStateName != null)
        {
            boss.SetVelocity(boss.moveSpeed * boss.facingDirection, boss.jumpForce);
        }
    }

    public override void Exit()
    {
        boss.bt.enabled = true;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0 && boss.IsGroundDetected())
        {
            boss.stateMachine.ChangeState(boss.landState);
        }
    }
}
