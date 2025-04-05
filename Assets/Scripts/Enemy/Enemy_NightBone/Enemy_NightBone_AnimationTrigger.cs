using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBone_AnimationTrigger : MonoBehaviour
{
    private Enemy_NightBone enmey => GetComponentInParent<Enemy_NightBone>();
    private void AnimationTrigger()
    {
        enmey.AnimationTrigger();
    }
}
