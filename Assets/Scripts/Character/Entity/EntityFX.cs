using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float flashDuration = .2f;
    private Material originMaterial;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originMaterial = sr.material;
    }

    private IEnumerator FlashFX()
    {
        if (sr!=null)
        {
            sr.material = hitMaterial;
        }
        yield return new WaitForSeconds(flashDuration);
        if (sr != null)
        {
            sr.material = originMaterial;
        }
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
