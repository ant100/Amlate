using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

    public float Damage { get => damage; set => damage = value; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}
