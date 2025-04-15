using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //用来找所有连接的函数
public class SaveManager : MonoBehaviour
{
    private GameData gameData;
    public static SaveManager instance;

    [SerializeField] private string fileName;
    [SerializeField] private bool encryptData;
    private List<ISaveManager> saveManagers;

    private FileDataHandler dataHandler;

    [ContextMenu("Delete save flie")]
    public void DeleteSaveData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        dataHandler.Delete();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        saveManagers = FindAllISaveManager();
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        gameData = dataHandler.Load();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        if (this.gameData == null)
        {
            NewGame();
        }
        else
        {
            foreach (ISaveManager saveManager in saveManagers)
            {
                if (saveManager != null)
                    saveManager.LoadData(this.gameData);
            }
        }

    }
    public void SaveGame()
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }
        dataHandler.Save(this.gameData);
    }

    //private void OnApplicationQuit()
    //{
    //    SaveGame();
    //}

    private List<ISaveManager> FindAllISaveManager()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }

    public bool HasSavedData()
    {
        if (dataHandler.Load() != null)
        {
            return true;
        }
        return false;
    }

    public bool HasEnemyData()
    {
        return gameData != null && gameData.enemyList != null && gameData.enemyList.Count > 0;
    }

    public List<GameData.EnemySaveData> GetEnemyData()
    {
        return gameData.enemyList;
    }

}
