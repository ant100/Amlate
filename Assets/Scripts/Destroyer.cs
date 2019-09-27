using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]
    private float time = 1.5f;

    void Awake()
    {
        Invoke("DestroyObject", time);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
