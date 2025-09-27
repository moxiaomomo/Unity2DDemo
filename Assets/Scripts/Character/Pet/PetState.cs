using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 宠物状态 
 */
public class PetState
{
    protected PetStateMachine stateMachine;
    protected PetOfPlayer pet;
    private string stateName;

    protected Rigidbody2D rb;
    protected bool triggeredCalled;
    protected float stateTimer;
    protected float xInput;
    protected float yInput;

    public PetState(PetOfPlayer _pet, PetStateMachine _stateMachine, string _stateName)
    {
        this.stateMachine = _stateMachine;
        this.pet = _pet;
        this.stateName = _stateName;
    }

    public virtual void Enter()
    {
        pet.animator.SetBool(stateName, true);
        rb = pet.rb;
        triggeredCalled = false;
    }

    public virtual void Exit() 
    {
        pet.animator.SetBool(stateName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        //xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");
        //pet.animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggeredCalled = true;
    }
}
