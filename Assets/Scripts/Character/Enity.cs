using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    [Header("Collison info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public Transform attackCheck;
    public float attackCheckRadius;

    public System.Action onFlipped; //用来调整血条UI的方向

    public int facingDirection { get; private set; } = 1;
    protected bool facingRight = true;

    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D capsulecd { get; private set; }
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        capsulecd = GetComponent<CapsuleCollider2D>();
    }

    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDirection, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    public virtual void Die()
    {

    }

    protected virtual void Update()
    {
    }
    #region Collison
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
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
    public virtual void SetZeroVelocity(bool isAttack = false)
    {
        if (isKnocked && !isAttack)
        {
            return;
        }
        rb.velocity = new Vector2(0, 0);
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
        {
            return;
        }
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
}
