using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_AnimationTrigger : MonoBehaviour
{
    private Enemy_NightBone enemy => GetComponentInParent<Enemy_NightBone>();

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
            if (damageable == null || damageable.Tag() == "NightBone")
            {
                continue;
            }
            damageable.TakeDamage(enemy.stats.offense.damage.GetValue(), enemy.transform);
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();

    private void EnemyDie()
    {
        EnemyPoolManager.instance.ReturnEnemy(enemy.poolTag, enemy);
    }
}
