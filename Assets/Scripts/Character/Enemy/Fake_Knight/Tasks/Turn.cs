using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


public class Turn : Fake_Knight_Action
{
    public override TaskStatus OnUpdate()
    {
        boss.Flip();

        return TaskStatus.Success;
    }
}