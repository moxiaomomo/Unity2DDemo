using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour, IDamageable
{
    private Slider healthBar;
    // private EntityFX entityFX;
    private Entity entity;
    private EntityStats stats;

    public bool isDead {  get; private set; }
    protected float _currentHp;
    protected float currentHP
    {
        get => _currentHp;
        set
        {
            if (value != _currentHp)
            {
                _currentHp = value;
                UpdateHealthBar();
            }
        }
    }

    protected virtual void Awake()
    {
        // entityFX = GetComponent<EntityFX>();
        entity = GetComponent<Entity>();
        stats = GetComponent<EntityStats>();
        healthBar = GetComponentInChildren<Slider>();

        currentHP = stats.GetMaxHealth();
    }

    public virtual string Tag()
    {
        return entity.poolTag;
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        ReduceHP(damage);

        // TODO recieveKnockback

        // TODO damageVFX
        entity.DamageEffect();
    }

    public virtual void IncreaseMaxHP(int _amount)
    {
        //maxHP.AddModifier(_amount, StatType.MaxHealth.ToString());
        //currentHP += _amount;
        //onHealthChanged?.Invoke();
        stats.resources.maxHP.AddModifier(_amount, StatType.MaxHealth.ToString());
        currentHP += _amount;
    }

    public void SetCurrentHP(float currentHp)
    {
        this.currentHP = currentHp;
    }

    public float GetCurrentHP()
    {
        return currentHP;
    }

    public virtual void Rebirth()
    {
        currentHP = (int)stats.resources.maxHP.GetValue();
    }

    protected void ReduceHP(float damage)
    {
        this.currentHP -= damage;
        if (this.currentHP < 0)
        {
            this.currentHP = 0;
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
            return;
        // healthBar.value = currentHp / stats.GetMaxHealth();
        healthBar.maxValue = stats.resources.maxHP.GetValue();
        healthBar.value = GetCurrentHP();
    }

    private void Die()
    {
        isDead = true;
    }
}
