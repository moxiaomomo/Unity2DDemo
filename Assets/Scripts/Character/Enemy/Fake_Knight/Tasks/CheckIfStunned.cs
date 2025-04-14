using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class CheckIfStunned : Fake_Knight_Conditional
{

    public override TaskStatus OnUpdate()
    {
        Debug.Log(boss.isStunned);
        return boss.isStunned ? TaskStatus.Success : TaskStatus.Failure;
    }


}
