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
        CurrentStage.Value++;
        if (CurrentStage.Value < 3)
        {

            boxcollider.enabled = true;
        }
        return TaskStatus.Success;
    }
}