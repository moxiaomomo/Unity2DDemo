using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public float maxHp;
    public float vitality;  // each poit give +5 HP

    public float GetMaxHealth()
    {
        float baseHp = maxHp;
        float bonusHp = vitality * 5;
        return baseHp + bonusHp;
    }

}
