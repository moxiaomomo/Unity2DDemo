using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetOfPlayer : Entity
{
    public Rigidbody2D pet;
    public Rigidbody2D player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pet = GetComponent<Rigidbody2D>();
        // //player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base .Update();
        if (player.velocity.x > 0)
        {
            pet.position = new Vector2(player.position.x - 2, player.position.y - 0.6f);
        } 
        else if (player.velocity.x < -0.01)
        {
            pet.position = new Vector2(player.position.x + 2, player.position.y - 0.6f);
        }
        pet.velocity = player.velocity;
        FlipController(player.velocity.x);
        handleAnimations();
    }

    public override void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            base.Flip();
        }
        else if (_x < -0.01 && facingRight)
        {
            base.Flip();
        }
    }

    private void handleAnimations()
    {
        bool isMoving = player.velocity.x != 0;
        animator.SetBool("isMoving", isMoving);
    }
}
