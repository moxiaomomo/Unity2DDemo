using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    void LoadGame(GameData _data);
    void SaveGame(GameData _data);

}
