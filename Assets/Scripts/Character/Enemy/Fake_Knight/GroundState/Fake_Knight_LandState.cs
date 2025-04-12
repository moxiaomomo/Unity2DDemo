using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_LandState : Fake_Knight_GroundState
{
    public Fake_Knight_LandState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
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
        if (rb.velocity.y == 0)
        {
            stateMachine.ChangeState(boss.idleState);
            boss.stateTrigger = true;
        }
    }
}
