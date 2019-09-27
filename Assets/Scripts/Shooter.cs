using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //[SerializeField]
    private float shootForce = 1000;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootBullet", 1f, 0.5f);
    }

    void ShootBullet() 
    {
        GameObject bulletObj2 = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletObj2.GetComponent<Rigidbody>().AddForce((player.GetComponent<Transform>().position - bulletObj2.GetComponent<Transform>().position).normalized * shootForce);
        //(player.position - bullet.position).normalized * constant;
    }
}
