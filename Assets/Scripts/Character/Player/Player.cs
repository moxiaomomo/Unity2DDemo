using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : PlayerBase
{
    public PlayerSkillManager skillManager { get; private set; }

    #region States
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallState wallState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    #endregion

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallState = new PlayerWallState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        skillManager = GetComponent<PlayerSkillManager>();
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        base.Update();
        CheckDashInput();
        CheckSkillInput();
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
}
