using System.Collections;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public float rotateTime = 0.2f;
    public bool isRotating {  get; private set; }
    private float curAngle = 0;
    private Transform player;

    private void Start()
    {
        isRotating = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = player.position;
        HandleRotate();
    }

    void HandleRotate()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            StartCoroutine(RotateAround(-45, rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateAround(45, rotateTime));
        }
    }

    IEnumerator RotateAround(float angle, float time)
    {
        float number = 60 * time;
        // 计算当前旋转角度（基于时间递增）
        float perAngel = angle / number;
        isRotating = true;

        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        for (int i = 0; i < number; i++)
        {
            transform.Rotate(new Vector3(0, 0, perAngel));
            yield return wait;
        }

        isRotating = false;
    }
}
