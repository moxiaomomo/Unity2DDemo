using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerGroundState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _stateName) : base(_player, _stateMachine, _stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //if (rb.velocity.y < 0)
        //{
        //    stateMachine.ChangeState(player.airState);
        //}
    }
}
