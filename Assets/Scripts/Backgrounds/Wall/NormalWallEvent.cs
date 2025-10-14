using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWallEvent : MonoBehaviour
{
    [Header("area info")]
    [SerializeField] private bool isDeathZone = true;
    [SerializeField] private bool isDamageZone = false;

    [Header("damage info")]
    [SerializeField] private float damageInterval = 1f; // 每几秒造成一次伤害
    [SerializeField] private int damageAmount = 5;

    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (isDeathZone)
        {
            PlayerManager.instance.player.Die();
        }

        if (isDamageZone)
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(ApplyDamageOverTime());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (isDamageZone && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        while (true)
        {
            PlayerManager.instance.player.health.TakeDamage(damageAmount, transform);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
