using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_AttackState : Fake_Knight_State
{
    public Fake_Knight_AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .5f;
        boss.animator.SetBool("attackAnticipate", true);

    }

    public override void Exit()
    {
        base.Exit();
        boss.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            boss.animator.SetBool("attackAnticipate", false);
            boss.SetZeroVelocity(isAttack: true);
            if (triggeredCalled)
            {
                boss.stateMachine.ChangeState(boss.idleState);
            }
        }
    }
}
