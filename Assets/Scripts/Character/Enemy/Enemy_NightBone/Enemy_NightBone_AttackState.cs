using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_AttackState : EnemyState
{
    private Enemy_NightBone enemy;
    public Enemy_NightBone_AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enemy) : base(_enemyBase, _stateMachine, _stateName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(7);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();
        if (triggeredCalled)
        {
            enemy.stateMachine.ChangeState(enemy.battleState);
        }
    }
}
