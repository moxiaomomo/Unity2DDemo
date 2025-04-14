using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_IdleState : EnemyState
{
    protected Enemy_NightBone enemy;
    public Enemy_NightBone_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enemy) : base(_enemyBase, _stateMachine, _stateName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
