using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maggot : Enemy
{
    private Fake_Knight boss;
    public bool stateTrigger = false;
    protected override void Start()
    {
        base.Start();
        boss = GetComponentInParent<Fake_Knight>();
    }

    protected override void Update()
    {
        if (stats.currentHP <= 0)
        {
            stateTrigger = true;   
            boss.stats.Rebirth();
            boss.stateTrigger = false;
            gameObject.SetActive(false);
        }
    }

}
