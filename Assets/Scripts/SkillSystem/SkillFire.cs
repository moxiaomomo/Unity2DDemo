using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SkillFire : SkillBase
{
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float detonateTime = 5;
    [SerializeField] private float cooldownTime = 0.1f;
    private float _cooldownTime = 0;

    public void CreateFire(bool facingRight)
    {
        if (_cooldownTime > 0)
        {
            return;
        }
        _cooldownTime = cooldownTime;
        GameObject fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
        fire.GetComponent<SkillObjectFire>().SetupFire(facingRight, detonateTime);
    }

    private void Update()
    {
        if (_cooldownTime >= 0)
        {
            _cooldownTime -= Time.deltaTime;
        }
    }

    //public void OnStartEffect()
    //{
    //    createClone();
    //}

    //private void createClone()
    //{

    //}
}
