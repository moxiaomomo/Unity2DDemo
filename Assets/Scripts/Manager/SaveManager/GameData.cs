using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerCurrency;
    public int playerCurrentHP;
    public Vector2 playerPosition;

    public List<EnemySaveData> enemyList = new List<EnemySaveData>();

    [System.Serializable]
    public class EnemySaveData
    {
        public string enemyID;
        public string enemyTag;
        public Vector3 position;
        public int currentHP;
        public bool isDead;
    }


    public GameData()
    {
        this.playerCurrency = 0;
        this.playerCurrentHP = 0;
        this.playerPosition = Vector2.zero;
    }
}
