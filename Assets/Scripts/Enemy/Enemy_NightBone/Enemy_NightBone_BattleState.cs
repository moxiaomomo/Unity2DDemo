using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_BattleState : EnemyState
{
    private Transform player;
    private int moveDiretion;
    private Enemy_NightBone enmey;
    public Enemy_NightBone_BattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enmey) : base(_enemyBase, _stateMachine, _stateName)
    {
        enmey = _enmey;
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

        if (enmey.IsPlayerDetected())
        {
            if (enmey.IsPlayerDetected().distance < enmey.attackDistance)
            {
                Debug.Log(enmey.IsPlayerDetected().distance);
                enmey.SetZeroVelocity();
                if (CanAttack())
                {
                    stateMachine.ChangeState(enmey.attackState);
                }
            }
        }

        if (player.position.x > enmey.transform.position.x)
        {
            moveDiretion = 1;
        }
        else if(player.position.x < enmey.transform.position.x)
        {
            moveDiretion = -1;
        }

        enmey.SetVelocity(enmey.moveSpeed * moveDiretion, rb.velocity.y);
    }


}
