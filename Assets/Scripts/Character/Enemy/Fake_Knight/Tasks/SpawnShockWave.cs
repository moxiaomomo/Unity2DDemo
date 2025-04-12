using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class SpawnShockWave : Fake_Knight_Action
{
    public string objectTag = "ShockWave";
    // Start is called before the first frame update
    public override void OnStart()
    {
        Generate();
    }

    private void Generate()
    {
        ObjectManager.instance.GetObjectItem(objectTag);
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
    public override void OnEnd()
    {

    }
}
