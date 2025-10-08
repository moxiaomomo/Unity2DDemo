using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class EnemySkeleton : Enemy
{

     #region State
    public EnemySkeletonIdleState idleState { get; private set; }
    public EnemySkeletonAttackState attackState { get; private set; }
    public EnemySkeletonBattleState battleState { get; private set; }
    public EnemySkeletonMoveState moveState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        poolTag = "Skeleton";
        idleState = new EnemySkeletonIdleState(this, stateMachine, "Idle");
        attackState = new EnemySkeletonAttackState(this, stateMachine, "Attack");
        battleState = new EnemySkeletonBattleState(this, stateMachine, "Move");
        moveState = new EnemySkeletonMoveState(this, stateMachine, "Move");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        RaycastHit2D playerDetected = IsPlayerDetected();
        if (playerDetected.collider != null)
        {
            stateMachine.ChangeState(attackState);
        } 
        else
        {
            stateMachine.ChangeState(idleState);
        }
    }

    /**
     * 能否被打晕
     */
    public override bool CanbeStunned()
    {
        if (base.CanbeStunned())
        {
            Debug.Log("Stunned");
            // stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

    public override RaycastHit2D IsPlayerDetected()
    {
        // 射线检测左右两个方向，返回较近的命中对象
        RaycastHit2D hitRight = Physics2D.Raycast(wallCheck.position, Vector2.right, playerCheckDistance, whatisPlayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(wallCheck.position, Vector2.left, playerCheckDistance, whatisPlayer);
        if (hitRight.collider != null)
        {
            return hitRight;
        }
        else if (hitLeft.collider != null)
        {
            return hitLeft;
        }

        return new RaycastHit2D(); // 返回一个空的结构体
    }

    public override void Die()
    {
        base.Die();
        // stateMachine.ChangeState(deadState);
    }
}
