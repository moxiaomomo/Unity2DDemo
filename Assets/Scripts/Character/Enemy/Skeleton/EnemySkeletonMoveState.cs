using UnityEngine;


public class EnemySkeletonMoveState : EnemyState
{
    private EnemySkeleton enemy;

    public EnemySkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName) : base(_enemyBase, _stateMachine, _stateName)
    {
        enemy = (EnemySkeleton)_enemyBase;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(7);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();
        if (triggeredCalled)
        {
            enemy.stateMachine.ChangeState(enemy.battleState);
        }
    }
}

