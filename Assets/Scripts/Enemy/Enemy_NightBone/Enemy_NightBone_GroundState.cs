using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enmey_NightBone_GroundState : EnemyState
{
    protected Enemy_NightBone enmey;
    public Enmey_NightBone_GroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Enemy_NightBone _enmey) : base(_enemyBase, _stateMachine, _stateName)
    {
        enmey = _enmey;
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
        if (enmey.IsPlayerDetected())
        {
            stateMachine.ChangeState(enmey.battleState);
        }
    }
}
