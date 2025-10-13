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
        _iniVeclociry = new Vector3(velocityX * (facingRight? 1:-1), 0, 0);
        Invoke(nameof(Explode), detinationTime);
    }

    private void Explode()
    {
        Destroy(gameObject);
        Instantiate(vfxPrefab, transform.position, Quaternion.identity);
    }

    private void OnEnemiesDetected(Collider2D[] colliders)
    {
        if (colliders.Length<=0)
            return;

        bool hasEnemyAlive = false;
        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy == null)
            {
                continue;
            }
            hasEnemyAlive = enemy.stats.GetCurrentHP()>0;
            if (hasEnemyAlive)
            {
                break;
            }
        }

        if (hasEnemyAlive)
        {
            // 触发爆炸动画
            Explode();
            // 计算伤害
            DoDamageEnemies(colliders);
        }
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
        Collider2D[] collders = EnemiesAround(transform, checkRadius);
        if (collders.Length > 0)
        {
            OnEnemiesDetected(collders);
        }
    }
}
