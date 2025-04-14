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
        dataHandler = new FileDataHandler(Application.persistentDataPath,fileName, encryptData);
        saveManagers = FindAllISaveManager();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if(this.gameData==null)
        {
            NewGame();
        }
        else
        {
            foreach (ISaveManager saveManager in saveManagers)
            {
                saveManager.LoadData(gameData);
            }
        }

    }
    public void SaveGame()
    {
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllISaveManager()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }

    public bool HasNoSavedData()
    {
        if(dataHandler.Load()!=null)
        {
            return true;
        }
        return false;
    }
}
