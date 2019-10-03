using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField]
    private float hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            //other.GetComponent<PlayerHealth>().hurtPlayer(hit);
        }
    }
}
