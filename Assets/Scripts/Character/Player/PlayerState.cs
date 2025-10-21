using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerBase player;
    public string stateName { get; private set; }

    protected Rigidbody2D rb;
    protected float xInput;
    protected float yInput;
    protected float stateTimer;
    protected bool triggeredCalled;

    public PlayerState(PlayerBase _player, PlayerStateMachine _stateMachine, string _stateName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.stateName = _stateName;
    }

    public virtual void Enter()
    {
        Enter(stateName);
    }

    public virtual void Enter(string _stateName)
    {
        this.stateName = _stateName;
        player.animator.SetBool(_stateName, true);
        rb = player.rb;
        triggeredCalled = false;
    }

    public virtual void Exit()
    {
        Exit(stateName);
    }

    public virtual void Exit(string _stateName)
    {
        player.animator.SetBool(_stateName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        if (player is Player)
        {
            player.animator.SetFloat("yVelocity", rb.velocity.y);
        }
    }

    public virtual void AnimationFinishTrigger()
    {
        triggeredCalled = true;
    }
}
