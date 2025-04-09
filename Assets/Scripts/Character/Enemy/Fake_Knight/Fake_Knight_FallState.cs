using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_FallState : EnemyState
{
    private Fake_Knight boss;

    public Fake_Knight_FallState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName)
    {
        boss = _boss;
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
        if(rb.velocity.y == 0)
        {
            stateMachine.ChangeState(boss.idleState);
        }
    }
}
