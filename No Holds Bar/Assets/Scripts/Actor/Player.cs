using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{

public class Player : MonoBehaviour
{
    public float max_pickup_distance = 2;
    public float hold_distance = 1.5f;
    public float throw_force = 1000.0f;

    Actor.Brawler me;
    Camera.Player eyes;
    Rigidbody pickup = null;
    float pickups_distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        me = this.GetComponent<Actor.Brawler>();
        eyes = this.GetComponentInChildren<Camera.Player>();
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

        if (Input.GetMouseButton(1))
        {
            if (null == pickup)
            {
                int layerMask = LayerMask.GetMask("Pickupables");
                RaycastHit hit;
                Transform transform = eyes.transform;
                
                // Debug.DrawRay(transform.position, eyes.transform.forward * 10, Color.yellow, 10, true);

                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, eyes.transform.forward, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.distance <= max_pickup_distance)
                    {
                        // Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 1000, Color.green, 10, true);           
                        // hit.rigidbody.AddForce(eyes.transform.forward * 20);
                        pickup = hit.rigidbody;
                        pickup.isKinematic = true;
                        pickups_distance = hit.distance;
                    }
                }
            }
              
        }

        if (Input.GetMouseButton(0))
        {
            if (null != pickup)
            {
                Debug.Log("throw");
                pickup.isKinematic = false;
                pickup.AddForce(me.transform.forward * throw_force, ForceMode.Force);
                pickup = null;
            }  
        }

        if (null != pickup)
        {
            // pickups_distance += (hold_distance - max_pickup_distance) * Time.deltaTime;
            pickup.position = eyes.transform.position + eyes.transform.forward * pickups_distance; 
        }

        me.Walk(dir.normalized);

        if (Input.GetKey("space"))
        {
            me.Jump();
        }
    }
}

}