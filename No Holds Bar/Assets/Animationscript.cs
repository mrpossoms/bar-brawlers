using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationscript : MonoBehaviour
{
    Animator animator;
    Rigidbody body;
    Actor.Brawler me;
    string anim_name = "isIdle";
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponentInParent<Rigidbody>();
        me = GetComponentInParent<Actor.Brawler>();
    }

    // Update is called once per frame
    void Update()
    {
        string last_anim_name = anim_name;

        if (me.IsAirborn())
        {
            anim_name = "isJump";
        }
        else if (body.velocity.magnitude > 0.01)
        {
            Vector3 nv = body.velocity.normalized;
            if (Vector3.Dot(nv, body.transform.forward) > 0.5)
            {
                anim_name = "isWalking";
            }
            else if (Vector3.Dot(nv, body.transform.forward) < -0.5)
            {
                anim_name = "isBackWalking";
            }

            if (Vector3.Dot(nv, body.transform.right) < -0.5)
            {
                anim_name = "isLeft";
            }
            else if (Vector3.Dot(nv, body.transform.right) > 0.5)
            {
                anim_name = "isRight";
            }
        }
        else
        {
            anim_name = "isIdle";
        }

        if (anim_name != last_anim_name)
        {
            animator.SetBool(last_anim_name, false);
        }

        animator.SetBool(anim_name, true);
    }




}
