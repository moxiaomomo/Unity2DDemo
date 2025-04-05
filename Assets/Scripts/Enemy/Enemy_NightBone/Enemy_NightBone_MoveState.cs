using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_MoveState : Enemy_NightBone_GroundState
{
    public Enemy_NightBone_MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enmey) : base(_enemyBase, _stateMachine, _stateName, _enmey)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.moveTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, rb.velocity.y);
        if(enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
        }
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
