using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_AnimationTrigger : MonoBehaviour
{
    private Fake_Knight boss => GetComponentInParent<Fake_Knight>();

    private void AnimationTrigger()
    {
        boss.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(boss.attackCheck.position, boss.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats _target = hit.GetComponent<PlayerStats>();
                boss.stats.DoDamage(_target);
            }
        }
    }

    private void OpenCounterWindow() => boss.OpenCounterAttackWindow();

    private void CloseCounterWindow() => boss.CloseCounterAttackWindow();

    private void EnemyDie()
    {
        EnemyPoolManager.instance.ReturnEnemy(boss.poolTag, boss);
    }
}
