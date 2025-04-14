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
        this.currency = _data.currency;
        player.stats.SetCurrentHP(_data.currentHP);
        player.transform.position = _data.position;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = this.currency;
        _data.currentHP = player.stats.GetCurrentHP();
        _data.position = player.transform.position;
    }
}
