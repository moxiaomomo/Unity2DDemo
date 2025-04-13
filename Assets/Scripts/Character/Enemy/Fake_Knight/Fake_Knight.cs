using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Fake_Knight : Enemy
{
    [Header("Pool Tag")]
    public string poolTag = "FakeKnight";

    public bool stateTrigger = false;

    #region State
    public Fake_Knight_IdleState idleState { get; private set; }
    public Fake_Knight_JumpState jumpState { get; private set; }
    public Fake_Knight_LandState landState { get; private set; }
    public Fake_Knight_MoveState moveState { get; private set; }
    public Fake_Knight_State attackState { get; private set; }
    public Fake_Knight_StunnedState stunnedState { get; private set; }
    public Fake_Knight_DeadState deadState { get; private set; }
    public Fake_Knight_JumpAnticipateState jumpAnticipateState { get; private set; }
    public Fake_Knight_ChangeStageState changeStageState { get; private set; }
    public Fake_Knight_AttackAnticipateState attackAnticipateState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new Fake_Knight_IdleState(this, stateMachine, "idle", this);
        jumpState = new Fake_Knight_JumpState(this, stateMachine, "jump", this);
        landState = new Fake_Knight_LandState(this, stateMachine, "land", this);
        moveState = new Fake_Knight_MoveState(this, stateMachine, "move", this);
        attackState = new Fake_Knight_AttackState(this, stateMachine, "attack", this);
        stunnedState = new Fake_Knight_StunnedState(this, stateMachine, "stunned", this);
        deadState = new Fake_Knight_DeadState(this, stateMachine, "dead", this);
        jumpAnticipateState = new Fake_Knight_JumpAnticipateState(this, stateMachine, "jumpAnticipate", this);
        changeStageState = new Fake_Knight_ChangeStageState(this, stateMachine, "changeStage", this);
        attackAnticipateState = new Fake_Knight_AttackAnticipateState(this, stateMachine, "attackAnticipate", this);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
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

}
