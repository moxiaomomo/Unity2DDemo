using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class CheckHealthUnder : Fake_Knight_Conditional
{
    public SharedInt HealthTreshold;

    public override TaskStatus OnUpdate()
    {
        float currentHP = boss.health.GetCurrentHP();
        return currentHP <= HealthTreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
    }

}
