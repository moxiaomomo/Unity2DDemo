using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : Entity
{
    public bool isBusy { get; private set; }
    public PlayerSkillManager skillManager { get; private set; }

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
    public float dashDiretion { get; private set; }

    [Header("Stunned info")]
    [SerializeField] private float force;
    [SerializeField] private float stunDuration = .5f;
    private bool isStunned = false;
    public bool IsStunned => isStunned;
    private Coroutine stunCoroutine;
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallState wallState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }

    public PlayerDeadState deadState { get; private set; }
    #endregion

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        poolTag = "Player";
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallState = new PlayerWallState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        deadState = new PlayerDeadState(this, stateMachine, "Die");

        skillManager = GetComponent<PlayerSkillManager>();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        base.Update();
        stateMachine.currentState.Update();
        CheckDashInput();
        CheckSkillInput();
    }

    //并流一个程序将人物设为繁忙，等待一定秒数后再释放它
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
    }
    private void CheckDashInput()
    {

        dashCoolTimeCounter -= Time.deltaTime;
        counterAttackCoolTimeCounter -= Time.deltaTime;

        if (IsWallDetected()||health.isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCoolTimeCounter < 0)
        {
            dashCoolTimeCounter = dashCoolTime;
            dashDiretion = Input.GetAxisRaw("Horizontal");
            if (dashDiretion == 0)
            {
                dashDiretion = facingDirection;
            }
            stateMachine.ChangeState(dashState);
        }
    }

    private void CheckSkillInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            skillManager.fire.CreateFire(facingRight);
        }
    }

    public override void DamageEffect()
    {
        base.DamageEffect();
        SetZeroVelocity();
        Vector2 knockback = new Vector2(-facingDirection, 1);
        rb.AddForce(knockback.normalized * force, ForceMode2D.Impulse);
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

        // 你可以在这里播放受控动画
        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
        stunCoroutine = null;
    }

}
