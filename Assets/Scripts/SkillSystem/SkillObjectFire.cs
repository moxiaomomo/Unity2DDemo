using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObjectFire : SkillObjectBase
{
    [SerializeField] private GameObject vfxPrefab;
    private Vector3 _iniVeclociry;
 //   private Rigidbody rb;

    public void SetupFire(bool facingRight, float detinationTime)
    {
        base.FlipController(facingRight ? 1 : -1);
        _iniVeclociry = new Vector3(3*(facingRight? 1:-1), 0, 0);
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

    protected override void Awake()
    {
        base.Awake();
        // rb = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // 更新位置和速度
        transform.position += _iniVeclociry * Time.deltaTime;

        // 检测碰撞
        Collider2D[] collder = EnemiesAround(transform, checkRadius);
        if (collder.Length > 0)
        {
            OnTriggerEnter2D (collder[0]);
        }
    }
}
