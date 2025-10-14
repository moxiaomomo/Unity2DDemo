using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [Header("Pool Tag")]
    [SerializeField] private string objectTag = "ShockWave";

    [Header("Object Info")]
    [SerializeField] private float maxTravelDistance = 10f;
    [SerializeField] private float xVelocity;
    [SerializeField] private int damage;

    public Rigidbody2D rb { get; private set; }
    public PolygonCollider2D polygonCollider { get; private set; }

    private Vector2 startPos;
    private int direction = 1;
    private bool isActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    public void Activate(Vector2 position, int dir)
    {
        direction = dir;
        transform.position = position;
        startPos = position;
        isActive = true;
        gameObject.SetActive(true);
        Debug.Log($"[ShockWave] Activated at {position}, dir: {direction}");
    }

    private void OnEnable()
    {
        if (!isActive)
        {
            // 如果没有通过 Activate 激活，自动初始化（保险）
            startPos = transform.position;
            direction = 1;
        }
    }

    private void Update()
    {
        if (!isActive) return;

        rb.velocity = new Vector2(xVelocity * direction, 0);

        if (Vector2.Distance(transform.position, startPos) >= maxTravelDistance)
        {
            Debug.Log("[ShockWave] Max distance reached, returning to pool.");
            isActive = false;
            ObjectManager.instance.ReturnObjectItem(objectTag, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[ShockWave] Hit player, dealing damage.");
            PlayerManager.instance.player.health.TakeDamage(damage, transform);
        }
    }
}
