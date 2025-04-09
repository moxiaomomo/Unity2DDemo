using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.animator.SetBool("SuccessCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanbeStunned())
                {
                    stateTimer = 10;
                    player.animator.SetBool("SuccessCounterAttack", true);
                }
            }
        }

        if (stateTimer < 0 || triggeredCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

