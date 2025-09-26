using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    private Player player;
    protected override void Start()
    {
        base.Start();
        // Debug.Log($"player currentHP: {currentHP}");
        player = GetComponent<Player>();
    }


    public override void TakeDamage(int _damage)
    {
        //Debug.Log($"player currentHP: {currentHP}");
        //Debug.Log($"player take_damage: {_damage}");
        base.TakeDamage(_damage);
        player.DamageEffect();
    }


    protected override void Die()
    {
        base.Die();
        player.Die();
    }

}
