using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour, IDamageable
{
    private Slider healthBar;
    private RectTransform rectTransform;
    private Entity entity;
    private EntityStats stats;

    [SerializeField] public bool isDead {  get; private set; }
    [SerializeField] protected float _currentHp;
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
        entity = GetComponent<Entity>();
        stats = GetComponent<EntityStats>();
        healthBar = GetComponentInChildren<Slider>();
        rectTransform = GetComponentInChildren<RectTransform>();
        currentHP = stats.GetMaxHealth();

        if (entity != null)
        {
            entity.onFlipped += FlipUI;
        }
    }

    private void OnDisable()
    {
        if (entity != null && entity.health.isDead)
            entity.onFlipped -= FlipUI;
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
        stats.resources.maxHP.AddModifier(_amount, StatType.MaxHealth.ToString());
        currentHP += _amount;
    }

    public void SetCurrentHP(float currentHp)
    {
        this.currentHP = currentHp;
        if (currentHP <= 0)
        {
            isDead = true;
        }
    }

    public float GetCurrentHP()
    {
        return currentHP;
    }

    public virtual void Rebirth()
    {
        currentHP = (int)stats.resources.maxHP.GetValue();
        isDead = false;
    }

    protected void ReduceHP(float damage)
    {
        this.currentHP -= damage;
        if (this.currentHP < 0)
        {
            this.currentHP = 0;
        }
        if (this.currentHP <= 0)
        {
            OnEntityDie();
        }
    }

    private void OnEntityDie()
    {
        isDead = true;
        entity.Die();
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
            return;
        healthBar.maxValue = stats.resources.maxHP.GetValue();
        healthBar.value = GetCurrentHP();
    }

    private void FlipUI()
    {
        if (rectTransform != null)
        {
            rectTransform.Rotate(0f, 180f, 0f);
        }
    }
}
