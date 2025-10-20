using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    private float inputX, inputY;

    // Start is called before the first frame update
    void Start()
    {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
        if (rigidbody != null)
        {
            rigidbody.transform.rotation = Camera.main.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(inputX, inputY);
        rigidbody.velocity = input;
    }
}
