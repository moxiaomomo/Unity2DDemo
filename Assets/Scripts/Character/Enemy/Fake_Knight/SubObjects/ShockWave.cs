using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [Header("Pool Tag")]
    [SerializeField] private string objectTag = "ShockWave";

    [Header("Object info")]
    [SerializeField] private float maxTravelDistance = 10f;
    [SerializeField] private LayerMask whatisGround;
    [SerializeField] private float xVelocity;
    [SerializeField] private int damage;

    private Fake_Knight boss;
    private Transform objectTransform;
    public Rigidbody2D rb { get; private set; }
    public PolygonCollider2D polygonCollider { get; private set; }
    private Vector2 startPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        boss = GetComponentInParent<Fake_Knight>();
        objectTransform = boss.attackCheck.transform;

    }

    private void OnEnable()
    {
        Debug.Log(boss);
        InitPosition();
        startPos = transform.position;
    }


    // Update is called once per frame
    private void Update()
    {
        rb.velocity = new Vector2(xVelocity * boss.facingDirection, 0);

        if (Vector3.Distance(transform.position, startPos) >= maxTravelDistance)
        {
            ObjectManager.instance.ReturnObjectItem(objectTag, gameObject);
        }
    }

    private void InitPosition()
    {
        // 从 objectTransform 向下发射一条射线，检测地面
        RaycastHit2D hit = Physics2D.Raycast(objectTransform.position, Vector2.down, 10f, whatisGround);

        if (hit.collider != null)
        {
            // 设置波的位置：X 使用攻击点，Y 使用地面高度
            transform.position = new Vector2(objectTransform.position.x, hit.point.y);
        }
        else
        {
            // 如果没检测到地面，保持原位置
            transform.position = objectTransform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.instance.player.stats.TakeDamage(damage); // 造成伤害
        }
    }

}
