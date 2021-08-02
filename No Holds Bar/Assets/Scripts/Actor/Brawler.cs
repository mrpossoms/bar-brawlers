using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Actor
{

public class Brawler : NetworkBehaviour
{
    public float seconds_between_jumps = 0.1f;
    private float walk_force = 200;
    private float jump_force = 10000;

    public float max_pickup_distance = 2;
    public float hold_distance = 1.5f;
    public float throw_force = 1000.0f;

    public float is_walking_speed_threshold = 0.1f;

    protected Rigidbody body; 
    private float cool_down_jump = 0;

    [SyncVar]
    GameObject pickup = null;
    
    [SyncVar]
    float pickups_distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    public Vector3 EyePosition()
    {
        return gameObject.transform.position + gameObject.transform.up * 2;    
    }

    public float walkingSpeed()
    {
        return (new Vector3(body.velocity.x, 0, body.velocity.z)).magnitude;
    }

    public void Walk(Vector2 dir)
    {
        float yaw = this.gameObject.transform.eulerAngles.y;//body.transform.eulerAngles.x;
        Vector3 force = Quaternion.AngleAxis(yaw, Vector3.up) * (new Vector3(dir.x, 0, dir.y) * walk_force);
     
        Debug.Log("Walking " + force);

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

    [Command]
    public void Pick()
    {
        if (null != pickup) { return; }

        int layerMask = LayerMask.GetMask("Pickupables");
        RaycastHit hit;
        // Transform transform = me.transform;        
        // Debug.DrawRay(transform.position, eyes.transform.forward * 10, Color.yellow, 10, true);

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(EyePosition(), transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.distance <= max_pickup_distance)
            {
                pickup = hit.rigidbody.gameObject;
                hit.rigidbody.isKinematic = true;
                pickups_distance = hit.distance;
                // return pickup;
            }
        }

        // return null;
    }

    [Command]
    public void Throw()
    {
        if (null != pickup)
        {
            Debug.Log("throw");
            Rigidbody rb = pickup.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(transform.forward * throw_force, ForceMode.Force);
            pickup = null;
        }  
    }


    void selectAnimations()
    {
        float speed = walkingSpeed();
        if (speed > is_walking_speed_threshold)
        {
            // Walking here
        }
        else
        {
            // Standing here (possibly falling)
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (null != pickup)
        {
            Rigidbody rb = pickup.GetComponent<Rigidbody>();
            // pickups_distance += (hold_distance - max_pickup_distance) * Time.deltaTime;
            rb.position = EyePosition() + transform.forward * pickups_distance; 
        }

        selectAnimations();

        cool_down_jump -= Time.deltaTime;
    }
}


}

