using UnityEngine;

// https://www.bilibili.com/video/BV1Zs3gzKEDP?spm_id_from=333.788.videopod.sections&vd_source=fad296d8bcd5e7e170ebe1a2e58ce22a&p=136
public class ObjectNPC : MonoBehaviour
{
    protected Transform player;
    protected UI ui;
    protected bool facingRight;

    [SerializeField] private string npcName;
    [SerializeField] private Transform npc;
    [SerializeField] private GameObject interactToolTip;

    [Header("Floatly Tooltip")]
    [SerializeField] private float floatSpeed = 8f;
    [SerializeField] private float floatRange = .1f;
    private Vector3 startPostion;

    private void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        startPostion = interactToolTip.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() == null)
        {
            return;
        }
        player = collision.transform;
        interactToolTip.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactToolTip.SetActive(false);
    }

    private void Update()
    {
        HandleToolTipFloat();
    }

    private void HandleToolTipFloat()
    {
        if (interactToolTip.activeSelf)
        {
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
            interactToolTip.transform.position = startPostion + new Vector3(0, yOffset, 0);
        }
    }
}
