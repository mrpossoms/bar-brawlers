using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{

public class Brawler : MonoBehaviour
{
    protected Rigidbody body; 

    public float walk_force = 100;
    public float jump_force = 1;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    public void Walk(Vector2 dir)
    {
        Debug.Log("Walking: " + dir);

        float yaw = this.gameObject.transform.eulerAngles.x;//body.transform.eulerAngles.x;
        Vector3 force = Quaternion.AngleAxis(yaw, Vector3.up) * (new Vector3(dir.x, 0, dir.y) * 100);
        Debug.Log("Walking force: " + force);

        GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
    }

    public void Jump()
    {
        body = GetComponent<Rigidbody>();
        if (Math.Abs(body.velocity.y) > 0.001)
        {
            //body.AddForce(new Vector3(0, jump_force, 0));            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


}

