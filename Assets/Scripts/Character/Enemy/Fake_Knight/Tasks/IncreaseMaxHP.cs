using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Diagnostics;
public class IncreaseMaxHP : Fake_Knight_Action
{
    public SharedInt CurrentStage;
    public int increaseHPStage;
    public int increaseHP;

    public override TaskStatus OnUpdate()
    {
        if(CurrentStage.Value >= increaseHPStage)
        {
            boss.stats.IncreaseMaxHP(increaseHP);
        }
        return TaskStatus.Success;
    }
}
