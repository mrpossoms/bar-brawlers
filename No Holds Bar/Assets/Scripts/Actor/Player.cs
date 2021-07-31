using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{

public class Player : MonoBehaviour
{
    Actor.Brawler me;

    // Start is called before the first frame update
    void Start()
    {
        me = this.GetComponent<Actor.Brawler>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = new Vector2(0, 0);
        if (Input.GetKey("w"))
        {
            dir += new Vector2(0, 1);
        }

        if (Input.GetKey("s"))
        {
            dir += new Vector2(0, -1);
        }

        if (Input.GetKey("a"))
        {
            dir += new Vector2(-1, 0);
        }

        if (Input.GetKey("d"))
        {
            dir += new Vector2(1, 0);
        }

        if (dir.magnitude > 0.0001)
        {
            Debug.Log(dir);
        }

        me.Walk(dir.normalized);

        if (Input.GetKey("space"))
        {
            me.Jump();
        }
    }
}

}