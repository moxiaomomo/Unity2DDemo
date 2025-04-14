using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class Fake_Knight_WallEvent : MonoBehaviour
{
    public string bossTag = "FakeKnight";
    public Transform bossSpawnPoint;
    public float bossSpawnDelay = 2f; // 暴露到 Inspector
    public GameObject saveBtn;

    private bool bossDefeated = false;
    public GameObject[] doorsToLock; // 把门设置在这里
    private bool triggered = false;

    [Header("change camera")]
    public CinemachineVirtualCamera cmPlayerCamera;
    public CinemachineVirtualCamera cmBossCamera;


    public static System.Action OnBossDefeated;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player") || other.transform.position.x < transform.position.x) return;

        triggered = true;
        AudioManager.instance.PlayBGM(1);
        saveBtn.SetActive(false); // 隐藏存档按钮

        StartCoroutine(DelayedSpawnBoss()); // 生成 Boss

        // 锁门
        foreach (var door in doorsToLock)
        {
            door.SetActive(true); // 门开启阻挡
        }

        cmPlayerCamera.Priority = 10;
        cmBossCamera.Priority = 20;
    }

    private IEnumerator DelayedSpawnBoss()
    {
        yield return new WaitForSeconds(bossSpawnDelay);

        // 生成 Boss
        Enemy boss = EnemyPoolManager.instance.GetEnemy(bossTag);
        boss.transform.position = bossSpawnPoint.position;
    }


    public void EndBossFight()
    {
        if (bossDefeated) return;

        AudioManager.instance.PlayBGM(0);
        bossDefeated = true;
        saveBtn.SetActive(true); // 隐藏存档按钮

        foreach (var door in doorsToLock)
        {
            door.SetActive(false); // 门打开
        }

        cmPlayerCamera.Priority = 20;
        cmBossCamera.Priority = 10;

        OnBossDefeated?.Invoke(); // 广播事件
    }

    private void OnEnable()
    {
        OnBossDefeated -= EndBossFight;
        OnBossDefeated += EndBossFight;
    }

    private void OnDisable()
    {
        OnBossDefeated -= EndBossFight;
    }

}