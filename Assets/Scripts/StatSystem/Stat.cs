using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat 
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    private float finalValue;

    public float GetValue()
    {
        return GetFinalValue();
    }

    public bool IsPositive()
    {
        return GetFinalValue()>0;
    }

    private float GetFinalValue()
    {
        float finalValue = baseValue;
        foreach (StatModifier modifier in modifiers) 
        {
            finalValue += modifier.value;
        }
        return finalValue;
    }

    public void AddModifier(float value, string source)
    {
        StatModifier modToAdd = new StatModifier(value, source);
        modifiers.Add(modToAdd);
    }

    public void RemoveModifier(string source)
    {
        modifiers.RemoveAll(modifier => modifier.source == source);
    }
}

[Serializable]
public class StatModifier
{
    public float value;
    public string source;

    public StatModifier(float value, string source)
    {
        this.value = value;
        this.source = source;
    }
}