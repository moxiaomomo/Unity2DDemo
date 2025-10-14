using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public string Tag();
    public void TakeDamage(float damage, Transform damageDealer);
}
