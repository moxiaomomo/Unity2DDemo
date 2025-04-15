using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public Player player;
    public int currency=0;
    public void Awake()
    {
        if(instance!=null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public int GetCurrency()=>currency;

    public void LoadData(GameData _data)
    {
        this.currency = _data.playerCurrency;
        player.stats.SetCurrentHP(_data.playerCurrentHP);
        player.transform.position = _data.playerPosition;
    }

    public void SaveData(ref GameData _data)
    {
        _data.playerCurrency = this.currency;
        _data.playerCurrentHP = player.stats.GetCurrentHP();
        _data.playerPosition = player.transform.position;
    }
}
