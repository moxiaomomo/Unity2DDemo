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
    public Stat repelledForce; // ������ʱ�����ܵ�����ֵ��<=0��ʾ��������

    // Elemental resistanse
    public Stat fireRes;
    public Stat iceRes;
    public Stat lightningRes;
}
