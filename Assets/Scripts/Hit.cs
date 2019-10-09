using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] private float hit = 1f;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }

        if(other.CompareTag("Player"))
        {
            // hurts player
            other.GetComponentInParent<PlayerHealth>().hurtPlayer(hit);
        }
    }
}
