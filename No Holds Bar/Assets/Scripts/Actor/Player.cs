using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Actor
{

public class Player : NetworkBehaviour
{

    Actor.Brawler me;

    // Start is called before the first frame update
    void Start()
    {
        me = this.GetComponent<Actor.Brawler>();
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 1.8f, 0);
    
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) { return; }

        float dt = Time.deltaTime;

        { // camera look
            float dx = Input.GetAxis("Mouse X");
            float dy = Input.GetAxis("Mouse Y");

            me.lookTilt(dx, dy);
            // Rigidbody parent_body = gameObject.GetComponentInParent(typeof(Rigidbody)) as Rigidbody;
            // if (null != parent_body)
            // {
            //     Camera.main.transform.eulerAngles -= (new Vector3(dy, 0, 0));
            //     parent_body.transform.eulerAngles -= (new Vector3(0, -dx, 0));
            // }
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

        float mag = dir.magnitude;
        if (mag > 0)
        {
            dir /= mag;
        }

        me.Walk(dir);

        bool mouse_right = Input.GetMouseButton(1);
        bool mouse_left = Input.GetMouseButton(0);

        if (mouse_right) { me.Pick(); }

        if (mouse_left) { me.Throw(); }

        if (mouse_left || mouse_right)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKey("space"))
        {
            me.Jump();
        }
    }
}

}