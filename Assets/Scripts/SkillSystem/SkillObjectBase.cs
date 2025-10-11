using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObjectBase : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float checkRadius = 1;

    protected void DamageEnemiesInRadius(Transform t, float radius)
    {
        // IDamegable
    }

    protected Collider2D[] EnemiesAround(Transform t, float radius)
    {
        return Physics2D.OverlapCircleAll(t.position, radius, whatIsEnemy);
    }

    protected virtual void onDrawGizmos()
    {
        if (targetCheck==null)
        {
            targetCheck = transform;
        }

        Gizmos.DrawWireSphere(targetCheck.position, checkRadius);
    }
}
