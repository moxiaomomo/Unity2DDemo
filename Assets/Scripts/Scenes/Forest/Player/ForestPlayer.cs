using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestPlayer : PlayerBase
{
    private float inputX, inputY;

    protected override void Awake()
    {
        base.Awake();
        if (rb != null)
        {
            rb.transform.rotation = Camera.main.transform.rotation;
        }
        idleState = new ForestPlayerIdleState(this, stateMachine, "Idle");
        moveState = new ForestPlayerMoveState(this, stateMachine, "Move");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rb.AddForce(new Vector2(0, 0.2f) * rb.mass);
        //}
    }
}
