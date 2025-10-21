using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerBase : Entity
{
    public bool isBusy { get; protected set; }

    [Header("Attack Info")]
    public Vector2[] attackMovement;
    public float counterAttackDuration;
    public float counterAttackCoolTime;
    public float counterAttackCoolTimeCounter;


    [Header("Move Info")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;


    [Header("Dash info")]
    public float dashCoolTime;
    public float dashCoolTimeCounter;
    public float dashSpeed = 10f;
    public float dashDuration;
    public float dashDiretion { get; protected set; }

    [Header("Stunned info")]
    [SerializeField] protected float force;
    [SerializeField] protected float stunDuration = .5f;
    public bool IsStunned => isStunned;
    protected Coroutine stunCoroutine;

    #region States
    public PlayerStateMachine stateMachine { get; protected set; }
    public PlayerState idleState { get; protected set; }
    public PlayerState moveState { get; protected set; }
    public PlayerState deadState { get; protected set; }

    #endregion

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        poolTag = "Player";
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    //����һ������������Ϊ��æ���ȴ�һ�����������ͷ���
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
        Debug.Log("Player is dead.");
    }

    public override void DamageEffect()
    {
        base.DamageEffect();
        StartStun();
    }

    public void StartStun()
    {
        if (stunCoroutine != null)
            StopCoroutine(stunCoroutine);

        stunCoroutine = StartCoroutine(StunRoutine());
    }

    private IEnumerator StunRoutine()
    {
        isStunned = true;

        // ����������ﲥ���ܿض���
        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
        stunCoroutine = null;
    }
}
