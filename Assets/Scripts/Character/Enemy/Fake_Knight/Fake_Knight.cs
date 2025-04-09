using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight : Enemy
{
    #region State
    public Fake_Knight_IdleState idleState { get; private set; }
    public Fake_Knight_FallState fallState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new Fake_Knight_IdleState(this, stateMachine, "idle", this);
        fallState = new Fake_Knight_FallState(this, stateMachine, "fall", this);
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


}
