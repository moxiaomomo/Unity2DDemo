using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    public EnemyStateMachine stateMachine;
    public Enemy enemyBase;
    private string stateName;
    protected bool triggeredCalled;
    protected float stateTimer;
    protected Rigidbody2D rb;
    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName)
    {
        stateMachine = _stateMachine;
        enemyBase = _enemyBase;
        stateName = _stateName;
    }
    public virtual void Enter() 
    {
        triggeredCalled = false;
        enemyBase.animator.SetBool(stateName, true);
        rb = enemyBase.rb;
    }
    public virtual void Exit() 
    {
        enemyBase.animator.SetBool(stateName, false);
    }
    public virtual void Update() 
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void AnimationFinishTrigger() 
    {
        triggeredCalled = true;    
    }

    public virtual bool CanAttack()
    {
        if (Time.time >= enemyBase.lastTimeAttacked + enemyBase.attackCoolDown)
        {
            enemyBase.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
