using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Enity
{
    public EnemyStateMachine stateMachine { get; private set; }

    [SerializeField]protected LayerMask whatisPlayer;

    [Header("Attack info")]
    public float playerCheckDistance;
    public float attackDistance;
    public float attackCoolDown;
    [HideInInspector]public float lastTimeAttacked;

    [Header("Move info")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float moveTime;
    [SerializeField] public float idleTime;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    public virtual RaycastHit2D IsPlayerDetected()=>Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerCheckDistance, whatisPlayer);

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDirection, transform.position.y));
    }

}
