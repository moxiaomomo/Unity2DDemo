using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Fake_Knight_Action
{
    public override void OnStart()
    {
        StartJump();
    }

    private void StartJump()
    {
        if (boss.stateMachine.currentState == null)
        {
            boss.stateMachine.Initialize(boss.jumpState); //初始化boss掉下来
        }
        else
        {
            boss.stateMachine.ChangeState(boss.jumpAnticipateState);
        }
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
