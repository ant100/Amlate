using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    private float minPosition = -34.0f;
    private float maxPosition = 34.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, minPosition, maxPosition), transform.position.y, transform.position.z);
    }
}
