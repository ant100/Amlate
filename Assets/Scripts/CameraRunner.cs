using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private float minPosition = 11.0f;
    private float maxPosition = -43.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(player.position.z, maxPosition, minPosition));
    }
}
