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

    // Elemental resistanse
    public Stat fireRes;
    public Stat iceRes;
    public Stat lightningRes;
}
