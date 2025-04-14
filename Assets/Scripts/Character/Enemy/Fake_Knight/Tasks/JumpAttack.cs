using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : Fake_Knight_Action
{
    public override void OnStart()
    {
        boss.stateMachine.ChangeState(boss.jumpAttackState);
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
