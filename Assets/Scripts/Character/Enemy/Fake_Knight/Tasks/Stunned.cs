using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class Stunned : Fake_Knight_Action
{
    public override void OnStart()
    {
        boss.stateMachine.ChangeState(boss.stunnedState);
    }

    public override TaskStatus OnUpdate()
    {
        boss.stateMachine.currentState.Update();
        return boss.stateTrigger ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {
        boss.stateTrigger = false;
        boss.ChangeStunned();
    }
}
