using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class SpawnShockWave : Fake_Knight_Action
{
    public string objectTag = "ShockWave";
    public LayerMask whatisGround;

    public override void OnStart()
    {
        Generate();
    }

    private void Generate()
    {
        GameObject shockWave = ObjectManager.instance.GetObjectItem(objectTag);

        if (shockWave == null)
        {
            Debug.LogWarning("[SpawnShockWave] 无法从对象池中获取 ShockWave");
            return;
        }

        Vector2 spawnPos = boss.attackCheck.position;

        // 使用 Raycast 精确贴地生成
        RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, 10f, whatisGround);
        if (hit.collider != null)
        {
            spawnPos = new Vector2(spawnPos.x, hit.point.y);
        }

        // 激活 ShockWave
        ShockWave wave = shockWave.GetComponent<ShockWave>();
        wave.Activate(spawnPos, boss.facingDirection);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        // 可选：加入结束时清理逻辑
    }
}
