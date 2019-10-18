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
    [SerializeField] private int numberOfProjectiles = 10;

    private float radius;
    private Vector3 startPoint;

    public float SpawningTime { get => spawningTime; set => spawningTime = value; }

    void Start()
    {
        radius = 5f;
        startPoint = transform.position;
        InvokeRepeating("ShootBullet", 1f, SpawningTime);
    }

    void ShootBullet() 
    {
        GameObject bulletObj = Instantiate(bullet, startPoint, Quaternion.identity);
        Vector3 projectileMoveDirection = (player.position - startPoint).normalized * shootForce;
        bulletObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, projectileMoveDirection.z);

        /*float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for(int i = 0; i <= numberOfProjectiles; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirZPosition = startPoint.z + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, startPoint.y, projectileDirZPosition);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * shootForce;

            var proj = Instantiate(bullet, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, projectileMoveDirection.z);

            angle += angleStep;
        }*/

        CameraShaker.Instance.ShakeOnce(1f, 5f, .1f, 1f);

    }
}
