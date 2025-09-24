using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string stateName;

    protected Rigidbody2D rb;
    protected float xInput;
    protected float yInput;
    protected float stateTimer;
    protected bool triggeredCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _stateName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.stateName = _stateName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(stateName, true);
        rb = player.rb;
        triggeredCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.animator.SetBool(stateName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggeredCalled = true;
    }
}
