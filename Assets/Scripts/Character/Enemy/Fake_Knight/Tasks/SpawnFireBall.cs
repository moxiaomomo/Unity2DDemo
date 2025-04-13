using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class SpawnFireBall : Fake_Knight_Action
{
    [Header("FireBall Info")]
    public string objectTag = "FireBall";
    public Transform fireBallWall;

    public float spawnInterval = .5f;

    private int fireBallCount;
    private Vector3 leftBottom;
    private Vector3 rightBottom;
    // Start is called before the first frame update
    public override void OnStart()
    {
        // 获取 fireBallCount
        foreach (var config in ObjectManager.instance.poolConfigs)
        {
            if (config.objectTag == objectTag)
            {
                fireBallCount = config.initialSize;
                break;
            }
        }
        // 获取生成线段坐标

        BoxCollider2D box = fireBallWall.GetComponent<BoxCollider2D>();

        if (box != null)
        {
            Bounds bounds = box.bounds;
            leftBottom = new Vector3(bounds.min.x, bounds.min.y, fireBallWall.position.z);  // 保持原始z
            rightBottom = new Vector3(bounds.max.x, bounds.min.y, fireBallWall.position.z);
        }

        // 启动协程生成火球
        StartCoroutine(SpawnFireBalls());
    }

    private IEnumerator SpawnFireBalls()
    {
        for (int i = 0; i < fireBallCount; i++)
        {
            SpawnSingleFireBall();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnSingleFireBall()
    {
        GameObject fireball = ObjectManager.instance.GetObjectItem(objectTag);
        float t = Random.Range(0f, 1f);
        Vector3 spawnPos = Vector3.Lerp(leftBottom, rightBottom, t);
        fireball.transform.position = spawnPos;
        fireball.transform.rotation = Quaternion.identity;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
    public override void OnEnd()
    {

    }
}
