using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class FreezeTime : Fake_Knight_Action
{
    public SharedFloat Duration = 0.5f;

    public override TaskStatus OnUpdate()
    {
        GameManager.instance.FreezeTime(Duration.Value);
        return TaskStatus.Success;
    }
}
