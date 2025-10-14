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
        AudioManager.instance.PlaySFX(2);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(boss.attackCheck.position, boss.attackCheckRadius);
        boss.PerformAttack(colliders);
    }

    private void OpenCounterWindow() => boss.OpenCounterAttackWindow();

    private void CloseCounterWindow() => boss.CloseCounterAttackWindow();

}
