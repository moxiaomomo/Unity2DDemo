using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatDefenseGroup
{
    // Physical defense
    public Stat armor;
    public Stat evasion;
    public Stat repelledForce; // 被击退时所接受的力量值，<=0表示不被击退

    // Elemental resistanse
    public Stat fireRes;
    public Stat iceRes;
    public Stat lightningRes;
}
