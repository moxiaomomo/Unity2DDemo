using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    // 基础属性
    public StatMajorGroup major;
    public StatOffenseGroup offense;
    public StatDefenseGroup defense;
    public StatResouceGroup resources;
    //// 额外加成属性
    //public StatMajorGroup extraMajor;
    //public StatOffenseGroup extraOffense;
    //public StatDefenseGroup extraDefense;
    //public StatResouceGroup extraResources;

    public float GetMaxHealth()
    {
        float baseHp = resources.maxHP.GetValue();
        float bonusHp = major.vitality.GetValue() * 5;
        return baseHp + bonusHp;
    }

    public Stat GetStatByType(StatType type)
    {
        switch(type)
        {
            case StatType.MaxHealth: return resources.maxHP;
            case StatType.HealthRegen: return resources.healthRegen;

            case StatType.Strength: return major.strength;
            case StatType.Agility: return major.agility;
            case StatType.Intelligence: return major.intelligence;
            case StatType.Vitality: return major.vitality;

            case StatType.AttackSpeed: return offense.attackSpeed;
            case StatType.Damage: return offense.damage;
            case StatType.CritChance: return offense.critChance;
            case StatType.CritPower: return offense.critPower;
            case StatType.ArmorReduction: return offense.armorReduction;

            case StatType.FireDamage: return offense.fireDamage;
            case StatType.IceDamage: return offense.iceDamage;
            case StatType.LightningDamage: return offense.lightningDamage;

            case StatType.Armor: return defense.armor;
            case StatType.Evasion: return defense.evasion;

            case StatType.FireResistance: return defense.fireRes;
            case StatType.IceResistance: return defense.iceRes;
            case StatType.LightningResistance: return defense.lightningRes;

            default:
                Debug.LogWarning($"StatType {type} not implement yet.");
                return null;
        }
    }
}
