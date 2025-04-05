using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone : Enemy
{
    #region State
    public Enemy_NightBone_IdleState idleState { get; private set; }
    public Enemy_NightBone_MoveState moveState { get; private set; }
    public Enemy_NightBone_BattleState battleState { get; private set; }
    public Enemy_NightBone_AttackState attackState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new Enemy_NightBone_IdleState(this, stateMachine, "Idle", this);
        moveState = new Enemy_NightBone_MoveState(this, stateMachine, "Move", this);
        battleState = new Enemy_NightBone_BattleState(this, stateMachine, "Move", this);
        attackState = new Enemy_NightBone_AttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
