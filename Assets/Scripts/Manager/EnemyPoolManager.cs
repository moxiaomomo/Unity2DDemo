using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager instance;

    [System.Serializable]
    public class EnemyPoolConfig
    {
        public string enemyTag;
        public Enemy prefab;
        public int initialSize = 5;
        public int maxSize = 20;
    }

    [System.Serializable]
    public class EnemySpawnConfig
    {
        public string enemyTag;
        public List<Transform> spawnPoints = new List<Transform>();
    }

    [Header("Enemy config")]
    [SerializeField] private List<EnemyPoolConfig> poolConfigs;
    [SerializeField] private List<EnemySpawnConfig> spawnConfigs;

    private Dictionary<string, ObjectPool<Enemy>> poolDict;
    private Dictionary<string, Enemy> activeEnemies = new Dictionary<string, Enemy>();
    private int enemyCounter = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        poolDict = new Dictionary<string, ObjectPool<Enemy>>();
        InitAllPools();
    }

    private void Start()
    {
        if (SaveManager.instance.HasEnemyData())
        {
            SpawnEnemiesFromSave(SaveManager.instance.GetEnemyData());
        }
        else
        // 只有在非读取存档时，才生成默认配置敌人
        //        if (!SessionFlags.isLoadingFromSave)

        {
            SpawnAllConfiguredEnemies();
        }
    }

    private void InitAllPools()
    {
        foreach (var config in poolConfigs)
        {
            ObjectPool<Enemy> pool = new ObjectPool<Enemy>(
                createFunc: () => {
                    var enemy = Instantiate(config.prefab);
                    enemy.poolTag = config.enemyTag;
                    enemy.gameObject.SetActive(false);
                    return enemy;
                },
                actionOnGet: enemy => {
                    enemy.gameObject.SetActive(true);
                },
                actionOnRelease: enemy => {
                    activeEnemies.Remove(enemy.enemyID);
                    enemy.gameObject.SetActive(false);
                },
                actionOnDestroy: enemy => {
                    Destroy(enemy.gameObject);
                },
                collectionCheck: true,
                defaultCapacity: config.initialSize,
                maxSize: config.maxSize
            );

            poolDict.Add(config.enemyTag, pool);
        }
    }

    public Enemy GetEnemy(string tag, string id = null)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogError($"敌人类型未注册：{tag}");
            return null;
        }

        // 如果指定 ID，并且已存在，直接返回现有敌人
        if (!string.IsNullOrEmpty(id) && activeEnemies.ContainsKey(id))
        {
            return activeEnemies[id];
        }

        // 否则从池中取新敌人
        var enemy = poolDict[tag].Get();

        // 防止重复 ID 的关键点
        enemy.enemyID = id ?? $"{tag}_{enemyCounter++}";

        // 加入前，检查是否已存在相同 ID
        if (!activeEnemies.ContainsKey(enemy.enemyID))
            activeEnemies[enemy.enemyID] = enemy;

        return enemy;
    }

    public void ReturnEnemy(string tag, Enemy enemy)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogWarning($"回收失败，敌人类型未注册：{tag}");
            return;
        }

        poolDict[tag].Release(enemy);
    }

    public List<Enemy> GetAllActiveEnemies()
    {
        return new List<Enemy>(activeEnemies.Values);
    }

    // <<<<<<< HEAD
    public void SpawnAllConfiguredEnemies()
    {
        foreach (var config in spawnConfigs)
        {
            foreach (var point in config.spawnPoints)
            {
                var enemy = GetEnemy(config.enemyTag);
                enemy.transform.position = point.position;
            }
        }
    }

    public void SpawnEnemiesFromSave(List<GameData.EnemySaveData> savedEnemies)
    {
        foreach (var data in savedEnemies)
        {
            if (data.isDead) continue;

            var enemy = GetEnemy(data.enemyTag, data.enemyID);
            if (enemy is IEnemySavable savable)
            {
                savable.LoadEnemySaveData(data);
                //=======
                //    public void SpawnEnemiesFromSave(List<GameData.EnemySaveData> savedEnemies)
                //    {
                //        foreach (var enemyData in savedEnemies)
                //        {
                //            var enemy = GetEnemy(enemyData.enemyTag);
                //            if (enemy == null)
                //            {
                //                Debug.LogError($"未能从对象池获取敌人：{enemyData.enemyTag}");
                //                continue;
                //            }

                //            enemy.enemyID = enemyData.enemyID; // 保留原ID
                //            if (enemy is IEnemySavable savable)
                //            {
                //                savable.LoadEnemySaveData(enemyData);
                //>>>>>>> 1c66aff (update readme)
            }
        }
    }
}
