using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXAutoController : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(sr, destroyDelay);
    }
}
