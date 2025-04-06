using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_BattleState : EnemyState
{
    private Transform player;
    private Enemy_NightBone enemy;
    private int moveDirection;
    public Enemy_NightBone_BattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enemy) : base(_enemyBase, _stateMachine, _stateName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        RaycastHit2D hit = enemy.IsPlayerDetected();

        if (hit.collider != null)
        {
            stateTimer = enemy.battleTime;

            if (hit.distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    enemy.stateMachine.ChangeState(enemy.attackState);
                    return;
                }
                else
                {
                    enemy.SetZeroVelocity(); //保证敌人不会与玩家重合造成抽搐
                    enemy.stateMachine.ChangeState(enemy.idleState);
                    return;
                }
            }
        }
        moveDirection = (player.transform.position.x > enemy.transform.position.x) ? 1 : -1;
        enemy.SetVelocity(moveDirection * enemy.moveSpeed, rb.velocity.y);

        if (stateTimer < 0 || Vector2.Distance(player.position, enemy.transform.position) > 20)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }

    }

}
