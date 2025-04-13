using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Fake_Knight_DeadState : Fake_Knight_State
{
    public Fake_Knight_DeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .8f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
        {
            boss.stateTrigger = true;
        }
    }

}
