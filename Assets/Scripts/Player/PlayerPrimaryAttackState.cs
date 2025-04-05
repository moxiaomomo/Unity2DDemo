using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboTime = .5f;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();


        if(comboCounter>2 || Time.time >= lastTimeAttacked + comboTime)
            comboCounter = 0;
        player.animator.SetInteger("ComboCounter", comboCounter);

        // 攻击不能移动，但可以换攻击方向
        float attackDirection = player.facingDirection;
        if (xInput != 0)
        {
            attackDirection = xInput;
        }

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDirection, player.attackMovement[comboCounter].y);

        stateTimer = .1f; //让攻击有移动惯性
    }

    public override void Exit()
    {
        base.Exit();

        //player.StartCoroutine("BusyFor", .2f); //让玩家攻击后有个停顿

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if(triggeredCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
