using System.Collections.Generic;
using UnityEngine;

public class EnemySaveManager : MonoBehaviour, ISaveManager
{
    public void SaveData(ref GameData data)
    {
        if (data.enemyList == null)
            data.enemyList = new List<GameData.EnemySaveData>();
        else
            data.enemyList.Clear();

        foreach (var enemy in EnemyPoolManager.instance.GetAllActiveEnemies())
        {
            if (enemy is IEnemySavable savable)
            {
                data.enemyList.Add(savable.GetEnemySaveData());
            }
        }
    }

    public void LoadData(GameData data)
    {
        if (data.enemyList == null || data.enemyList.Count == 0) return;

        EnemyPoolManager.instance.SpawnEnemiesFromSave(data.enemyList);
    }
}
