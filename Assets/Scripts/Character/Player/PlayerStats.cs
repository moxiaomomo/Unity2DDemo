using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerStats : EntityStats
{
    // Start is called before the first frame update
    private Player player;

    protected virtual void Awake()
    {
        player = GetComponent<Player>();
    }
}
