using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    // Start is called before the first frame update

    [SerializeField] private float parallaxEffect;

    private float xPosition;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distacneBGMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distacneBGMove, transform.position.y);
    }
}
