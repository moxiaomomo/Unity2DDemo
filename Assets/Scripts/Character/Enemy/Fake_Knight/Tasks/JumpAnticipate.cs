using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class JumpAnticipate : Fake_Knight_Action
{
    // Start is called before the first frame update
    public override void OnStart()
    {
        boss.stateMachine.ChangeState(boss.jumpAnticipateState);
    }


    public override TaskStatus OnUpdate()
    {
        boss.stateMachine.currentState.Update();
        return boss.stateTrigger ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {
        boss.stateTrigger = false;
    }
}
