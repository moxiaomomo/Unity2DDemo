using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PetOfPlayer : Entity
{
    public Rigidbody2D petRb;
    public Rigidbody2D playerRb;

    // private PlayerState playerState;
    public static UnityEvent<string> playerStateChanged;

    /*
     * 攻击属性 
     */
    [Header("Attack Info")]
    public Vector2[] attackMovement;
    public float counterAttackDuration;
    public float counterAttackCoolTime;
    public float counterAttackCoolTimeCounter;

    #region States
    public PetStateMachine stateMachine { get; private set; }
    public PetIdleState idleState { get; private set; }
    public PetMovingState movingState { get; private set; }
    public PetAttackState attackState { get; private set; }
    public PetSitState sitState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        // 获取玩家Animator状态
        //Animator playerAnimator = player.GetComponentInChildren<Animator>();
        //AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(stateInfo.IsName("Base Layer.playerIdle"));
        // 获取Player脚本实例
        // Player player = GetComponent<Player>();
        playerStateChanged = new UnityEvent<string>();
        playerStateChanged.AddListener(onPlayerStateChanged);

        stateMachine = new PetStateMachine();
        idleState = new PetIdleState(this, stateMachine, "Idle");
        movingState = new PetMovingState(this, stateMachine, "Move");
        attackState = new PetAttackState(this, stateMachine, "Attack");
        sitState = new PetSitState(this, stateMachine, "Sit");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        petRb = GetComponent<Rigidbody2D>();
        // //player = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);

        // Enemy[] enemies = GetComponents<Enemy>();
        //List<Enemy> enemies = EnemyPoolManager.instance.GetAllActiveEnemies();
        //Debug.Log(enemies.Count);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        petRb.velocity = playerRb.velocity;
        FlipController(playerRb.velocity.x);
        if (playerRb.velocity.x > 0)
        {
            petRb.position = new Vector2(playerRb.position.x - 3, playerRb.position.y - 0.8f);
        } 
        else if (playerRb.velocity.x < -0.01)
        {
            petRb.position = new Vector2(playerRb.position.x + 3, playerRb.position.y - 0.8f);
        }
        else
        {
            petRb.position = new Vector2(petRb.position.x, playerRb.position.y - 0.8f);
        }
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

    /**
     * 跟随Player状态改变Pet状态
     */
    private void onPlayerStateChanged(string stateName)
    {
        //GameObject[] elist = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log(elist.Length);
        switch (stateName)
        {
            case "Idle":
                stateMachine.ChangeState(idleState);
                break;
            case "Move":
                stateMachine.ChangeState(movingState);
                break;
            case "Jump":
                stateMachine.ChangeState(sitState);
                
                break;
            case "Attack":
                stateMachine.ChangeState(attackState);
                break;
        }
        // 消除动画引起y轴位置的改变问题
        petRb.position = new Vector2(petRb.position.x, playerRb.position.y - 0.6f);
    }
}
