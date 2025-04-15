using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class SpawnMaggot : Fake_Knight_Action
{

    public SharedGameObject maggotShared;
    private Maggot maggot;
    public SharedInt currentStage;

    public override void OnStart()
    {
        maggot = maggotShared.Value.GetComponent<Maggot>();
        maggot.SetCurrentStage(currentStage.Value);
        maggot.gameObject.SetActive(true);
    }

    public override TaskStatus OnUpdate()
    {
        return maggot.stateTrigger ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {
        maggot.stateTrigger = false;
    }
}
