using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySavable
{
    GameData.EnemySaveData GetEnemySaveData();
    void LoadEnemySaveData(GameData.EnemySaveData data);
}
