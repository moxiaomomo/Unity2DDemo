using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
//    ForestPlayer player;
    Transform[] childs;


    // Start is called before the first frame update
    void Start()
    {
        //GameObject obj = GameObject.FindGameObjectWithTag("Player");
        //player = obj.GetComponent<ForestPlayer>();
        childs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
            childs[i].rotation = Camera.main.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.tag == "Player")
        //{
        //    if (player.rb.velocity.x != 0)
        //    {
        //        player.FlipController(player.rb.velocity.x);
        //    }
        //    Quaternion original = Camera.main.transform.rotation;
        //    original.y = transform.rotation.y;
        //    transform.rotation = original;
        //    // transform.rotation = Camera.main.transform.rotation;
        //    return;
        //}

        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].rotation = Camera.main.transform.rotation;
        }
    }
}
