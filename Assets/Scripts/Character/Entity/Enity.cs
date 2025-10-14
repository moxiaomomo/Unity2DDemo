using System.Collections;
using System.Collections.Generic;
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
    // 地面图层蒙版，可用于player jump时的判断
    [SerializeField] protected LayerMask whatIsGround;
    public Transform attackCheck;
    public float attackCheckRadius;

    public System.Action onFlipped; //用来调整血条UI的方向

    public int facingDirection { get; private set; } = 1;
    protected bool facingRight = true;

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
    }

    protected virtual void Start()
    {
    }

    public virtual void DamageEffect()
    {
        if (!gameObject.activeInHierarchy) return;
        fx.StartCoroutine("FlashFX");
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

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
}
