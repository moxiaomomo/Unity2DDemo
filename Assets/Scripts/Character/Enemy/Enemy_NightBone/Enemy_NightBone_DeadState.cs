using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_DeadState : EnemyState
{
    private Enemy_NightBone enemy;
    public Enemy_NightBone_DeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enemy) : base(_enemyBase, _stateMachine, _stateName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.animator.SetBool(enemy.lastStateName, true);
        //stateTimer = .8f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(0, rb.velocity.y);
        //if (stateTimer < 0)
        //{
        //    //enemy.animator.speed = 0;
        //    //enemy.capsulecd.enabled = false;
        //    GameObject.Destroy(enemy.gameObject, 0.5f);
        //}
    }
}
