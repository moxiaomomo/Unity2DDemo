using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_GroundState : Fake_Knight_State
{
    public Fake_Knight_GroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
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
    }
}
