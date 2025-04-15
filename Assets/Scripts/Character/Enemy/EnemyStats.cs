using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Enemy enemy;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        if (GetCurrentHP() > 0)
        {
            enemy.DamageEffect();
        }
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
