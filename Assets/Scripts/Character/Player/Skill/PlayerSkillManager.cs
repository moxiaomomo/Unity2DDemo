using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public SkillFire fire {  get; private set; }

    private void Awake()
    {
        fire = GetComponentInChildren<SkillFire>();
    }

    public SkillBase GetSkillObjectByType(SkillType type)
    {
        switch(type)
        {
            case SkillType.Fire:
                return fire;
            default:
                return null;
        }
    }
}
