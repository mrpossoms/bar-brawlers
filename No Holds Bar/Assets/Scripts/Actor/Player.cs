using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Actor
{

public class Player : NetworkBehaviour
{
    public float max_pickup_distance = 2;
    public float hold_distance = 1.5f;
    public float throw_force = 1000.0f;

    Actor.Brawler me;
    Rigidbody pickup = null;
    float pickups_distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        me = this.GetComponent<Actor.Brawler>();
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 2, 0);
    
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    Vector3 eye_pos()
    {
        return Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (null != pickup)
        {
            // pickups_distance += (hold_distance - max_pickup_distance) * Time.deltaTime;
            pickup.position = eye_pos() + me.transform.forward * pickups_distance; 
        }


        if (!isLocalPlayer) { return; }

        float dt = Time.deltaTime;

        { // camera look
            float dx = Input.GetAxis("Mouse X");
            float dy = Input.GetAxis("Mouse Y");

            Rigidbody parent_body = gameObject.GetComponentInParent(typeof(Rigidbody)) as Rigidbody;
            if (null != parent_body)
            {
                parent_body.transform.eulerAngles -= (new Vector3(dy, -dx, 0));
            }
        }

        if (Input.GetKey("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

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

        bool mouse_right = Input.GetMouseButton(1);
        bool mouse_left = Input.GetMouseButton(0);

        if (mouse_right)
        {
            if (null == pickup)
            {
                int layerMask = LayerMask.GetMask("Pickupables");
                RaycastHit hit;
                Transform transform = me.transform;
                
                // Debug.DrawRay(transform.position, eyes.transform.forward * 10, Color.yellow, 10, true);

                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(eye_pos(), transform.forward, out hit, Mathf.Infinity, layerMask))
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

        if (mouse_left)
        {
            if (null != pickup)
            {
                Debug.Log("throw");
                pickup.isKinematic = false;
                pickup.AddForce(me.transform.forward * throw_force, ForceMode.Force);
                pickup = null;
            }  
        }

        if (mouse_left || mouse_right)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        me.Walk(dir.normalized);

        if (Input.GetKey("space"))
        {
            me.Jump();
        }
    }
}

}