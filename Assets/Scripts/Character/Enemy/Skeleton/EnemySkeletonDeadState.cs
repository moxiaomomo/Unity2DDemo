using UnityEngine;


public class EnemySkeletonDeadState : EnemyState
{
    private EnemySkeleton enemy;

    public EnemySkeletonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName) : base(_enemyBase, _stateMachine, _stateName)
    {
        enemy = (EnemySkeleton)_enemyBase;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(0);
    }

    public override void Exit()
    {
        base.Exit();
        // enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();
    }
}

