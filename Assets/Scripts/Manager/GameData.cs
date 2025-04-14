using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currency;
    public int currentHP;
    public Vector2 position;

    public GameData()
    {
        this.currency = 0;
        this.currentHP = 0;
        this.position = Vector2.zero;
    }
}
