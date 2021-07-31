using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{

public class Player : Brawler
{
    private RigidBody body; 

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

}