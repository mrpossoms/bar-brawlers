using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Actor
{

public class Brawler : NetworkBehaviour
{
    protected Rigidbody body; 
    private float cool_down_jump = 0;

    public float seconds_between_jumps = 0.1f;
    public float walk_force = 200;
    public float jump_force = 10000;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    public void Walk(Vector2 dir)
    {
        float yaw = this.gameObject.transform.eulerAngles.y;//body.transform.eulerAngles.x;
        Vector3 force = Quaternion.AngleAxis(yaw, Vector3.up) * (new Vector3(dir.x, 0, dir.y) * walk_force);
     
        body.AddForce(force, ForceMode.Force);
    }

    public void Jump()
    {
        if (Math.Abs(body.velocity.y) < 0.0001 && cool_down_jump <= 0)
        {
            body.AddForce(new Vector3(0, jump_force, 0));
            cool_down_jump = seconds_between_jumps;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cool_down_jump -= Time.deltaTime;
    }
}


}

