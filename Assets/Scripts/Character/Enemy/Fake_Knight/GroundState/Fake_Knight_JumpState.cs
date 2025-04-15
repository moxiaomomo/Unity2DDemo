using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_JumpState : Fake_Knight_GroundState
{

    public Fake_Knight_JumpState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .5f;
        // 初始化boss下落不进行加速度
        if (boss.lastStateName != null)
        {
            Transform player = PlayerManager.instance.player.transform;
            float playerDistance = Mathf.Abs(player.position.x - boss.transform.position.x);

            float jumpTime = boss.jumpForce / Mathf.Abs(Physics2D.gravity.y); // 上升时间
            float jumpHorizontalDistance = boss.moveSpeed * jumpTime;

            // 如果玩家已经很近，就精准跳跃到玩家位置
            if (playerDistance < jumpHorizontalDistance)
            {
                float horizontalSpeed = (player.position.x - boss.transform.position.x) / jumpTime;
                boss.SetVelocity(horizontalSpeed, boss.jumpForce);
            }
            else
            {
                // 使用默认的固定朝向跳跃
                boss.SetVelocity(boss.moveSpeed * boss.facingDirection, boss.jumpForce);
            }
        }
    }

    public override void Exit()
    {
        boss.bt.enabled = true;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0 && boss.IsGroundDetected())
        {
            boss.stateMachine.ChangeState(boss.landState);
        }
    }
}
