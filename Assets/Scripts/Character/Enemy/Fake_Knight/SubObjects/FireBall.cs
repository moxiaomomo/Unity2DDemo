using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Pool Tag")]
    [SerializeField] private string objectTag = "FireBall";

    [Header("Object info")]
    [SerializeField] private int damage;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public Rigidbody2D rb { get; private set; }
    public CircleCollider2D circleCollider { get; private set; }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }
    private  bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    // Update is called once per frame
    private void Update()
    {

        if (IsGroundDetected())
        {
            ObjectManager.instance.ReturnObjectItem(objectTag, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.instance.player.stats.TakeDamage(damage); // ‘Ï≥……À∫¶
        }
    }
}
