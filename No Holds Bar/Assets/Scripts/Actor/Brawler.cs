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
    private float walk_force = 5;
    private float jump_force = 250;
    public float air_movement_control = 0.25f;

    public float max_pickup_distance = 2;
    public float hold_distance = 1.5f;
    public float throw_force = 20.0f;

    public float is_walking_speed_threshold = 0.1f;

    protected Rigidbody body; 
    private float cool_down_jump = 0;

    [SyncVar]
    private Vector3 look_eulers = new Vector3(0, 0, 0);

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
        return gameObject.transform.position + Camera.main.transform.localPosition;    
    }

    public Vector3 HandPosition()
    {
        return gameObject.transform.position + Camera.main.transform.localPosition - (new Vector3(0, 0.25f, 0));    
    }

    public bool IsAirborn()
    {
        return !Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }

    public float walkingSpeed()
    {
        return (new Vector3(body.velocity.x, 0, body.velocity.z)).magnitude;
    }

    public Vector3 lookDirection()
    {
        Quaternion q = Quaternion.Euler(look_eulers);
        return q * Vector3.forward;
    }

    [Command]
    void submitLook(Vector3 e)
    {
        look_eulers = e;
    }

    public void lookTilt(float d_yaw, float d_pitch)
    {
        look_eulers -= (new Vector3(d_pitch, -d_yaw, 0));
        Camera.main.transform.eulerAngles = new Vector3(look_eulers.x, look_eulers.y, 0);
        this.gameObject.transform.eulerAngles = new Vector3(0, look_eulers.y, 0);
        submitLook(look_eulers);
    }

    public void Walk(Vector2 dir)
    {
        float yaw = this.gameObject.transform.eulerAngles.y;//body.transform.eulerAngles.x;
        Vector3 force = Quaternion.AngleAxis(yaw, Vector3.up) * (new Vector3(dir.x, 0, dir.y) * walk_force);
     
        if (IsAirborn())
        {
            force *= air_movement_control;
        }

        print("Walking " + force);

        body.AddForce(force, ForceMode.Impulse);
    }

    public void Jump()
    {
        if (Math.Abs(body.velocity.y) < 0.0001 && cool_down_jump <= 0)
        {
            body.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
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
        if (Physics.Raycast(EyePosition(), lookDirection(), out hit, Mathf.Infinity, layerMask))
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
            rb.AddForce(lookDirection() * throw_force, ForceMode.Impulse);
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
            rb.position = HandPosition() + lookDirection() * pickups_distance; 
        }

        Debug.DrawRay(EyePosition(), lookDirection() * 10, Color.yellow, 1, true);

        selectAnimations();

        cool_down_jump -= Time.deltaTime;
    }
}


}

