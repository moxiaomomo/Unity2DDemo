using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enmey_NightBone_GroundState : EnemyState
{
    protected Enemy_NightBone enemy;
    public Enmey_NightBone_GroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enemy) : base(_enemyBase, _stateMachine, _stateName)
    {
        enemy = _enemy;
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
        RaycastHit2D hit = enemy.IsPlayerDetected();
        if (hit.collider!=null)
        {
            stateMachine.ChangeState(enemy.battleState);
            return;
        }
    }
}
