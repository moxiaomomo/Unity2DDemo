using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header ("Major stats")]
    public Stat strength;


    public Stat damage;
    public Stat maxHP;

    protected int currentHP;

    public System.Action onHealthChanged;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHP = (int)maxHP.GetValue();
        //Debug.Log($"currentHP: {currentHP}");
    }


    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = (int)strength.GetValue() + (int)damage.GetValue();
        if (_targetStats != null)
        {
            _targetStats.TakeDamage(totalDamage);
        }
    }

    // Update is called once per frame
    public virtual void TakeDamage(int _damage)
    {
        currentHP -= _damage;
        if (currentHP < 0) 
        {
            currentHP = 0;
        }
        onHealthChanged?.Invoke();
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void Rebirth()
    {
        currentHP = (int)maxHP.GetValue();
        onHealthChanged?.Invoke();
    }

    public virtual void IncreaseMaxHP(int _amount)
    {
        maxHP.AddModifier(_amount, StatType.MaxHealth.ToString());
        currentHP += _amount;
        onHealthChanged?.Invoke();
    }

    public virtual int GetCurrentHP()
    {
        return currentHP;
    }

    public virtual void SetCurrentHP(int _amount)
    {
        currentHP = _amount;
        onHealthChanged?.Invoke();
    }

    protected virtual void Die()
    {
    }
}
