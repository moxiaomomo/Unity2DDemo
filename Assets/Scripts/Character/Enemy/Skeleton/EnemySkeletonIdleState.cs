using UnityEngine;


public class EnemySkeletonIdleState : EnemyState
{
    private EnemySkeleton enemy;

    public EnemySkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName) : base(_enemyBase, _stateMachine, _stateName)
    {
        enemy = (EnemySkeleton)_enemyBase;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}

