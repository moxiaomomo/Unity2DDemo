using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maggot : Enemy
{
    private Fake_Knight boss;
    private bool flipOnce = false;
    public bool stateTrigger = false;
    private BoxCollider2D boxcollider;
    private int currentStage = 0;
    protected override void Start()
    {
        base.Start();
        boss = GetComponentInParent<Fake_Knight>();
    }

    public void SetCurrentStage(int _stage)
    {
        currentStage = _stage;
    }

    protected override void Update()
    {
        FlipUI();
        boxcollider = boss.animator.GetComponentInChildren<BoxCollider2D>();
        boxcollider.enabled = false;
    }

    public override void Die()
    {
        base.Die();
        boss.health.Rebirth();
        boss.stateTrigger = false;

        stateTrigger = true;
        flipOnce = false;
        health.Rebirth();
        gameObject.SetActive(false);
    }

    private void FlipUI()
    {
        if (flipOnce) return;
        if (boss.facingDirection != 1 && !flipOnce)
        {
            flipOnce = true;
            onFlipped?.Invoke();
        }
    }

}
