using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class Fake_Knight_Conditional : Conditional
{
    protected Fake_Knight boss;
    protected Player player;


    public override void OnAwake()
    {
        base.OnAwake();
        player = PlayerManager.instance.player;
        boss = GetComponent<Fake_Knight>();
    }
}
