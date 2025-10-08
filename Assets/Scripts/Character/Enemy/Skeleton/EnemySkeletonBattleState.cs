using UnityEngine;


public class EnemySkeletonBattleState : EnemyState
{
    private EnemySkeleton enemy;
    private Transform player;
    private int moveDirection;

    public EnemySkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName) : base(_enemyBase, _stateMachine, _stateName)
    {
        enemy = (EnemySkeleton)_enemyBase;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        RaycastHit2D hit = enemy.IsPlayerDetected();

        if (hit.collider != null)
        {
            stateTimer = enemy.battleTime;
            if (hit.distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    enemy.stateMachine.ChangeState(enemy.attackState);
                    return;
                }
                else
                {
                    enemy.stateMachine.ChangeState(enemy.idleState);
                    return;
                }
            }
        }
        moveDirection = (player.position.x >= enemy.transform.position.x) ? 1 : -1;
        enemy.SetVelocity(moveDirection * enemy.moveSpeed, rb.velocity.y);

        if (stateTimer < 0 || Vector2.Distance(player.position, enemy.transform.position) > 20)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }

    }
}

