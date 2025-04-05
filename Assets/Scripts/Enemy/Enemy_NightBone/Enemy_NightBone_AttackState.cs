using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_AttackState : EnemyState
{
    private Enemy_NightBone enmey;
    public Enemy_NightBone_AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enmey) : base(_enemyBase, _stateMachine, _stateName)
    {
        enmey = _enmey;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enmey.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enmey.SetZeroVelocity();
        if (triggeredCalled)
            stateMachine.ChangeState(enmey.battleState);
    }
}
