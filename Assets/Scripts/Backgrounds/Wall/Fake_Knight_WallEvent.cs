using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class Fake_Knight_WallEvent : MonoBehaviour
{
    public string bossTag = "FakeKnight";
    public Transform bossSpawnPoint;
    public float bossSpawnDelay = 2f; // ��¶�� Inspector
    public GameObject saveBtn;

    private bool bossDefeated = false;
    public GameObject[] doorsToLock; // ��������������
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
        saveBtn.SetActive(false); // ���ش浵��ť

        StartCoroutine(DelayedSpawnBoss()); // ���� Boss

        // ����
        foreach (var door in doorsToLock)
        {
            door.SetActive(true); // �ſ����赲
        }

            cmPlayerCamera.Priority = 10;
            cmBossCamera.Priority = 20;
    }

    private IEnumerator DelayedSpawnBoss()
    {
        yield return new WaitForSeconds(bossSpawnDelay);

        // ���� Boss
        Enemy boss = EnemyPoolManager.instance.GetEnemy(bossTag);
        boss.transform.position = bossSpawnPoint.position;
    }


    public void EndBossFight()
    {
        if (bossDefeated) return;

        AudioManager.instance.PlayBGM(0);
        bossDefeated = true;
        saveBtn.SetActive(true); // ���ش浵��ť

        foreach (var door in doorsToLock)
        {
            door.SetActive(false); // �Ŵ�
        }

        cmPlayerCamera.Priority = 20;
        cmBossCamera.Priority = 10;

        OnBossDefeated?.Invoke(); // �㲥�¼�
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