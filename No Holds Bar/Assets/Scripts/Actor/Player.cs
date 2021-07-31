using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{

public class Player : Brawler
{
    private Rigidbody body; 

    public float walk_force = 1;
    public float jump_force = 1;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Walk(Vector2 dir)
    {
        body.AddForce(dir * walk_force, ForceMode.Force);
    }

    void Jump()
    {
        if (Math.Abs(body.velocity.y) > 0.001)
        {
            body.AddForce(new Vector3(0, jump_force, 0));            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

}