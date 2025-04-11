using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Fake_Knight : Enemy
{
    [Header("Pool Tag")]
    public string poolTag = "FakeKnight";

    #region State
    public Fake_Knight_IdleState idleState { get; private set; }
    public Fake_Knight_JumpState fallState { get; private set; }
    public Fake_Knight_LandState landState { get; private set; }
    public Fake_Knight_MoveState moveState { get; private set; }
    public Fake_Knight_State attackState { get; private set; }
    public Fake_Knight_StunnedState stunnedState { get; private set; }
    public Fake_Knight_DeadState deadState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new Fake_Knight_IdleState(this, stateMachine, "idle", this);
        fallState = new Fake_Knight_JumpState(this, stateMachine, "jump", this);
        landState = new Fake_Knight_LandState(this, stateMachine, "land", this);
        moveState = new Fake_Knight_MoveState(this, stateMachine, "move", this);
        attackState = new Fake_Knight_AttackState(this, stateMachine, "attack", this);
        stunnedState = new Fake_Knight_StunnedState(this, stateMachine, "stunned", this);
        deadState = new Fake_Knight_DeadState(this, stateMachine, "dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(fallState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanbeStunned()
    {
        if (base.CanbeStunned())
        {
            Debug.Log("Stunned");
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
