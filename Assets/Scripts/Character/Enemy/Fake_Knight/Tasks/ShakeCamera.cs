using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ShakeCamera : Fake_Knight_Action
{
    [Header("震动参数")]
    public SharedFloat shakeStrength = 1f;

    [BehaviorDesigner.Runtime.Tasks.Tooltip("0 = Vertical（上下），1 = Horizontal（左右）")]
    public SharedInt shakeDirection = 0;

    public override TaskStatus OnUpdate()
    {
        var shake = boss.GetComponent<CameraShake>();
        if (shake != null)
        {
            // 将 SharedInt 转为 enum
            CameraShake.ShakeDirection dir = (CameraShake.ShakeDirection)shakeDirection.Value;

            shake.Shake(shakeStrength.Value, dir);
        }
        else
        {
            Debug.LogWarning("CameraShake 组件未挂在 Boss 身上！");
        }

        return TaskStatus.Success;
    }
}
