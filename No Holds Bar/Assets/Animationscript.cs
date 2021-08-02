using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationscript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            animator.SetBool("isWalking", true);
        }

        if (!Input.GetKey("w"))
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey("s"))
        {
            animator.SetBool("isBackWalking", true);
        }

        if (!Input.GetKey("s"))
        {
            animator.SetBool("isBackWalking", false);
        }
        if (Input.GetKey("a"))
        {
            animator.SetBool("isLeft", true);
        }

        if (!Input.GetKey("a"))
        {
            animator.SetBool("isLeft", false);
        }
        if (Input.GetKey("d"))
        {
            animator.SetBool("isRight", true);
        }

        if (!Input.GetKey("d"))
        {
            animator.SetBool("isRight", false);
        }

        if (Input.GetKey("space"))
        {
            animator.SetBool("isJump", true);
        }

        if (!Input.GetKey("space"))
        {
            animator.SetBool("isJump", false);
        }

    }




}
