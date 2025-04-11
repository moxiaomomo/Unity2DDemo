using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_MoveState : Fake_Knight_GroundState
{
    public Fake_Knight_MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
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
        RaycastHit2D hit = boss.IsPlayerDetected();

        if (hit.collider != null)
        {
            if (hit.distance < boss.attackDistance)
            {
                if (CanAttack())
                {
                    boss.stateMachine.ChangeState(boss.attackState);
                    return;
                }
                else
                {
                    boss.stateMachine.ChangeState(boss.idleState);
                    return;
                }
            }
        }
        boss.SetVelocity(boss.moveSpeed * boss.facingDirection, rb.velocity.y);

    }

}
