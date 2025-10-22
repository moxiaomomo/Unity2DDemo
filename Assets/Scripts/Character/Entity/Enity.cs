using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Pool Tag")]
    public string poolTag;

    [Header("Collison info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    // ����ͼ���ɰ棬������player jumpʱ���ж�
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] public Transform attackCheck;
    [SerializeField] public float attackCheckRadius;
    [SerializeField] public int facingDirection { get; private set; } = 1;
    [SerializeField] protected bool facingRight = true;
    [SerializeField] public bool isStunned = false;
    //// ������2.5D������ģ������ʱʹ��
    //[SerializeField] public float zVelocity = 0;
    //public Vector3 rbInitialPosition {  get; private set; }

    public System.Action onFlipped; //��������Ѫ��UI�ķ���

    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx;
    public CapsuleCollider2D capsulecd { get; private set; }

    // public CharacterStats stats { get; private set; }
    public EntityStats stats { get; private set; }
    public EntityHealth health { get; private set; }
    #endregion

    protected virtual void Awake()
    {
        fx = GetComponent<EntityFX>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // stats = GetComponent<CharacterStats>();
        capsulecd = GetComponent<CapsuleCollider2D>();

        stats = GetComponent<EntityStats>();
        health = GetComponent<EntityHealth>();

        //if (rb!=null)
        //{
        //    rbInitialPosition = rb.transform.position;
        //}
    }

    protected virtual void Start()
    {
    }

    public bool CanMove()
    {
        return !isStunned;
    }

    public virtual void DamageEffect()
    {
        if (!gameObject.activeInHierarchy) return;
        fx.StartCoroutine("FlashFX");

        if (stats.defense.repelledForce.IsPositive())
        {
            Vector2 knockback = new Vector2(-facingDirection, 1);
            rb.AddForce(knockback.normalized * stats.defense.repelledForce.GetValue(), ForceMode2D.Impulse);
        }
    }

    public virtual void PerformAttack(Collider2D[] targets)
    {
        foreach (var hit in targets)
        {
            IDamageable targetDamageable = hit.GetComponent<IDamageable>();
            if (targetDamageable == null || targetDamageable.Tag() == this.poolTag)
            {
                continue;
            }
            targetDamageable.TakeDamage(stats.offense.damage.GetValue(), transform);
        }
    }

    public virtual void Die()
    {
        health.SetCurrentHP(0);
    }

    protected virtual void Update()
    {
    }
    #region Collison
    protected virtual void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        }
        if (wallCheck != null) 
        {
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        }
        if (attackCheck != null) 
        {
            Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
        }
    }

    public virtual bool IsGroundDetected() {
        return groundCheck != null && 
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    public virtual bool IsWallDetected() 
    {
        return wallCheck != null &&
        Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    } 
    #endregion

    #region Flip
    public virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

        onFlipped?.Invoke(); // Notify all listeners that the entity has flipped
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
    #endregion

    #region Velocity
    public virtual void SetZeroVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity, bool autoFlipX = true)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        if(autoFlipX)
        {
            FlipController(_xVelocity);
        }
    }
    #endregion
}
