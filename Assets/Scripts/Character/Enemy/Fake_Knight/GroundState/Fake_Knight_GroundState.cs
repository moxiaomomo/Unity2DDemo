using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_GroundState : Fake_Knight_State
{
    private Transform player;
    private int moveDirection;
    public Fake_Knight_GroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
    {
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
}
