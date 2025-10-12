using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFire : SkillBase
{
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float detonateTime = 2;

    public void CreateFire(bool facingRight)
    {
        GameObject fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
        fire.GetComponent<SkillObjectFire>().SetupFire(facingRight, detonateTime);
    }

    //public void OnStartEffect()
    //{
    //    createClone();
    //}

    //private void createClone()
    //{

    //}
}
