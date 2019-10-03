﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //[SerializeField]
    //private float punchAttackForce = 1f;
    private Animator animator;
    private bool hitEnded = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(hitEnded)
            DoAttack();
    }

    private void DoAttack()
    {
        if (Input.GetKey("x"))
        {
            // do animation
            animator.SetTrigger("Hit");
        }

        if (Input.GetKeyUp("x"))
        {
            animator.ResetTrigger("Hit");
        }
    }


    private void EndHit()
    {
        hitEnded = true;
    }

    private void StartHit()
    {
        hitEnded = false;
    }
}