using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


public class GoToNextStage : Fake_Knight_Action
{
    public SharedInt CurrentStage;

    public override TaskStatus OnUpdate()
    {
        CurrentStage.Value++;
        return TaskStatus.Success;
    }
}