using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_StunnedState : Fake_Knight_State
{
    public Fake_Knight_StunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        boss.fx.InvokeRepeating("RedColorBlink", 0, .5f);

        rb.velocity = new Vector2(-boss.facingDirection * boss.stunDirection.x, boss.stunDirection.y);
        stateTimer = boss.stunDuration;
    }

    public override void Exit()
    {
        base.Exit();
        boss.fx.Invoke("CancelRedBlink", 0);
    }
    public override void Update()
    {
        base.Update();
        Debug.Log(stateTimer);
        if (stateTimer < 0)
        {
            boss.stateTrigger = true;
            stateMachine.ChangeState(boss.idleState);
        }
    }
}
