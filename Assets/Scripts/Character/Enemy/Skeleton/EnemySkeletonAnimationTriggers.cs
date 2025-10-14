using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAnimationTriggers : MonoBehaviour
{
    private EnemySkeleton enemy => GetComponentInParent<EnemySkeleton>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (var hit in colliders)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable == null || damageable.Tag()=="Skeleton")
            {
                continue;
            }
            damageable.TakeDamage(enemy.stats.offense.damage.GetValue(), enemy.transform);
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();

    // for skeleton enemy
    private void DieTrigger()
    {
        enemy.gameObject.SetActive(false);
    }
}
