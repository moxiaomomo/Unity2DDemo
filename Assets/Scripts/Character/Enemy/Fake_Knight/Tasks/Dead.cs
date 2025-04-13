using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class Dead : Fake_Knight_Action
{

    public override TaskStatus OnUpdate()
    {
        boss.Die();
        return TaskStatus.Success;
    }

}
