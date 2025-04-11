using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_State : EnemyState
{
    protected Fake_Knight boss;
    private Transform player;
    private int moveDirection;
    public Fake_Knight_State(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName)
    {
        boss = _boss;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        moveDirection = (player.position.x >= boss.transform.position.x) ? 1 : -1;
        if (moveDirection != boss.facingDirection)
        {
            boss.Flip();
        }
    }

    public override bool CanAttack()
    {
        if (Time.time >= boss.lastTimeAttacked + boss.attackCooldown)
        {
            return true;
        }
        return false;
    }
}
