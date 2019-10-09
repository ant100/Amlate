using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private WeaponStats[] weapons = null;
    [SerializeField] private string changeWeaponKey = "z";

    private Animator animator;
    private bool hitEnded = true;
    private WeaponStats activeWeapon = null;
    private float punchForce;
    private int weaponIndex = 0;
    [HideInInspector] public bool isAttacking;

    public float PunchForce { get => punchForce; set => punchForce = value; }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isAttacking = false;
        activeWeapon = weapons[0];
        PunchForce = weapons[0].Force;
    }

    private void Update()
    {
        if (Input.GetKey(changeWeaponKey))
            SetActiveWeapon();

        if (hitEnded)
            DoAttack();
    }

    private void SetActiveWeapon()
    {
        if(weaponIndex + 1 < weapons.Length)
        {
            weaponIndex++;
        }
        else
        {
            weaponIndex = 0;
        }

        activeWeapon = weapons[weaponIndex];
        PunchForce = weapons[weaponIndex].Force;
    }

    private void DoAttack()
    {
        if (Input.GetKey(activeWeapon.Key))
        {
            // do animation
            animator.SetTrigger(activeWeapon.AnimationTrigger);
        }  
    }

    private void EndHit()
    {
        hitEnded = true;
        isAttacking = false;
    }

    private void StartHit()
    {
        hitEnded = false;
    }

    private void IsPunching()
    {
        isAttacking = true;
    }
}
