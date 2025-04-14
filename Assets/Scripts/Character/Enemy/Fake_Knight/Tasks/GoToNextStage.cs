using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


public class GoToNextStage : Fake_Knight_Action
{
    public SharedInt CurrentStage;
    private BoxCollider2D boxcollider;

    public override void OnStart()
    {
       boxcollider = boss.animator.GetComponentInChildren<BoxCollider2D>();
    }

    public override TaskStatus OnUpdate()
    {
        boxcollider.enabled = true;
        CurrentStage.Value++;
        return TaskStatus.Success;
    }
}