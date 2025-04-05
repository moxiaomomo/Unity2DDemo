using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_IdleState : EnemyState
{
    private Enemy_NightBone enmey;
    public Enemy_NightBone_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enemy) : base(_enemyBase, _stateMachine, _stateName)
    {
        enmey = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enmey.SetZeroVelocity();
        stateTimer = enmey.idleTime;
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
            stateMachine.ChangeState(enmey.moveState);
        }
    }
}
