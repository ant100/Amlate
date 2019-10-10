using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float shootForce = 1000;
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform player = null;
    [SerializeField] private float spawningTime = 1.5f;

    void Start()
    {
        InvokeRepeating("ShootBullet", 1f, spawningTime);
    }

    void ShootBullet() 
    {
        GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletObj.GetComponent<Rigidbody>().AddForce((player.position - bulletObj.GetComponent<Transform>().position).normalized * shootForce);
        CameraShaker.Instance.ShakeOnce(1f, 5f, .1f, 1f);
        //CameraShaker.Instance.StartShake(3f, 4f, .1f);
    }
}
