using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fake_Knight_ChangeStageState : Fake_Knight_State
{
    private Transform player;
    private int moveDirection;
    public Fake_Knight_ChangeStageState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .5f;
        player = PlayerManager.instance.player.transform;
        moveDirection = (player.position.x >= boss.transform.position.x) ? 1 : -1;
        boss.rb.velocity = new Vector2(boss.moveSpeed * -moveDirection * 2 , 0);
    }

    public override void Exit()
    {
        base.Exit();
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
        {
            boss.stateTrigger = true;
        }
    }
}
