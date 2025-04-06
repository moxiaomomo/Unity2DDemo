using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_MoveState : Enmey_NightBone_GroundState
{
    public Enemy_NightBone_MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enmey) : base(_enemyBase, _stateMachine, _stateName, _enmey)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enmey.moveTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enmey.SetVelocity(enmey.moveSpeed * enmey.facingDirection, rb.velocity.y);
        if (enmey.IsWallDetected() || !enmey.IsGroundDetected())
        {
            enmey.Flip();
        }
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(enmey.idleState);
        }
    }
}
