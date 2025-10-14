using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStats
{
    Enemy enemy;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemy = GetComponent<Enemy>();
    }
}
