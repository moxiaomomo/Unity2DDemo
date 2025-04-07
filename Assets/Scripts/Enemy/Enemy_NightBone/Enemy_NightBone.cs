
using UnityEngine;

public class Enemy_NightBone : Enemy
{
    #region State
    public Enemy_NightBone_IdleState idleState { get; private set; }
    public Enemy_NightBone_MoveState moveState { get; private set; }
    public Enemy_NightBone_BattleState battleState { get; private set; }
    public Enemy_NightBone_AttackState attackState { get; private set; }
    public Enemy_NightBone_StunnedState stunnedState { get; private set; }
    public Enemy_NightBone_DeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new Enemy_NightBone_IdleState(this, stateMachine, "Idle", this);
        moveState = new Enemy_NightBone_MoveState(this, stateMachine, "Move", this);
        battleState = new Enemy_NightBone_BattleState(this, stateMachine, "Move", this);
        attackState = new Enemy_NightBone_AttackState(this, stateMachine, "Attack", this);
        stunnedState = new Enemy_NightBone_StunnedState(this, stateMachine, "Stunned", this);
        deadState = new Enemy_NightBone_DeadState(this, stateMachine, "Die", this);
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

    public override RaycastHit2D IsPlayerDetected()
    {
        // 射线检测左右两个方向，返回较近的命中对象
        RaycastHit2D hitRight = Physics2D.Raycast(wallCheck.position, Vector2.right, playerCheckDistance, whatisPlayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(wallCheck.position, Vector2.left, playerCheckDistance, whatisPlayer);

        if (hitRight.collider != null && hitLeft.collider != null)
        {
            return hitRight.distance < hitLeft.distance ? hitRight : hitLeft;
        }
        else if (hitRight.collider != null)
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
        stateMachine.ChangeState(deadState);
    }
}
