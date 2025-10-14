using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour, IDamgable
{
    private Slider healthBar;
    private EntityFX entityFX;
    private Entity entity;
    private EntityStats stats;

    [SerializeField] protected float currentHp;
    [SerializeField] private bool isDead;

    protected virtual void Awake()
    {
        entityFX = GetComponent<EntityFX>();
        entity = GetComponent<Entity>();
        stats = GetComponent<EntityStats>();
        healthBar = GetComponent<Slider>();

        currentHp = stats.GetMaxHealth();
        UpdateHealthBar();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        // TODO recieveKnockback

        // TODO damageVFX

        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        if (healthBar == null)
            return;
        healthBar.value = currentHp / stats.maxHp;
    }

    private void Die()
    {
        isDead = true;
    }

    private void UpdateHealthBar()
    {

    }
}
