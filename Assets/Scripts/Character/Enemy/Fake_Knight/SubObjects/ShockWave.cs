using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [Header("Pool Tag")]
    [SerializeField] private string objectTag = "ShockWave";

    [Header("Object info")]
    [SerializeField] private float maxTravelDistance = 10f;
    [SerializeField] private float xVelocity;
    [SerializeField] private int damage;
    
    public Rigidbody2D rb { get; private set; }
    public PolygonCollider2D polygonCollider { get; private set; }
    private Vector2 startPos;
    private int direction = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    private void OnEnable()
    {
        startPos = transform.position;
    }


    // Update is called once per frame
    private void Update()
    {
        rb.velocity = new Vector2(xVelocity * direction, 0);

        if (Vector2.Distance(transform.position, startPos) >= maxTravelDistance)
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
