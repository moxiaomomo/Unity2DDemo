using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObjectBase : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float checkRadius = 1;
    [SerializeField] protected int damage = 50;
    [SerializeField] protected int velocityX = 5;

    public int facingDirection { get; private set; } = 1;
    protected bool facingRight = true;

    protected void DoDamageEnemies(Collider2D[] enemies)
    {
        foreach (Collider2D collider in enemies)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy == null || enemy.stats.GetCurrentHP()<=0)
            {
                continue;
            }
            enemy.stats.TakeDamage(damage);
        }
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

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    public virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

        // onFlipped?.Invoke(); // Notify all listeners that the entity has flipped
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
}
