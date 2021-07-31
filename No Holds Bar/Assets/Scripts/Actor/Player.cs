using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{

public class Player : Brawler
{

    // Start is called before the first frame update
    void Start()
    {

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
            dir += new Vector2(1, 0);
        }

        if (Input.GetKey("d"))
        {
            dir += new Vector2(-1, 0);
        }

        if (dir.magnitude > 0.0001)
        {
            Debug.Log(dir);
        }

        this.GetComponent<Actor.Brawler>().Walk(dir.normalized);

        if (Input.GetKey("space"))
        {
            this.GetComponent<Actor.Brawler>().Jump();
        }        
    }
}

}