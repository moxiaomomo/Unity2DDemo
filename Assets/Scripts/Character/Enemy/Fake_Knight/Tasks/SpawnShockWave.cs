using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class SpawnShockWave : Fake_Knight_Action
{
    public string objectTag = "ShockWave";
    public LayerMask whatisGround;
    private GameObject shockWave;
    // Start is called before the first frame update
    public override void OnStart()
    {
        Generate();
    }

    private void Generate()
    {
        shockWave = ObjectManager.instance.GetObjectItem(objectTag);

        RaycastHit2D hit = Physics2D.Raycast(boss.attackCheck.position, Vector2.down, 10f, whatisGround);

        if (hit.collider != null)
        {
            // 设置波的位置：X 使用攻击点，Y 使用地面高度
            shockWave.transform.position = new Vector2(boss.attackCheck.position.x, hit.point.y);
        }
        else
        {
            // 如果没检测到地面，保持原位置
            shockWave.transform.position = boss.attackCheck.position;
        }

        //通过方法注入参数
        var wave = shockWave.GetComponent<ShockWave>();
        wave.SetDirection(boss.facingDirection);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
    public override void OnEnd()
    {

    }
}
