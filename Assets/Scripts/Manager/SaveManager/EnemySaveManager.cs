using System.Collections.Generic;
using UnityEngine;

public class EnemySaveManager : MonoBehaviour, ISaveManager
{
    public void SaveData(ref GameData data)
    {
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
        if (data.enemyList == null) return;

        foreach (var enemyData in data.enemyList)
        {
            Enemy enemy = EnemyPoolManager.instance.GetEnemy(enemyData.enemyTag);
            if (enemy is IEnemySavable savable)
            {
                savable.LoadEnemySaveData(enemyData);
            }
        }
    }
}
