using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObjectFire : SkillObjectBase
{
    [SerializeField] private GameObject vfxPrefab;
    private GameObject fireExplosion;

    public void SetupFire(float detinationTime)
    {
        Invoke(nameof(Explode), detinationTime);
    }

    private void Explode()
    {
        DamageEnemiesInRadius(transform, checkRadius);
        Destroy(gameObject);
        Instantiate(vfxPrefab, transform.position, Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() == null)
            return;

        Explode();
    }
}
