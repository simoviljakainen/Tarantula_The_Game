using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float minShootingInterval;
    public bool autoShoot, forceDirection;
    public float speed = 200f;

    private float time = 0;
    private GameObject shotProjectile;
    private Rigidbody rb;
    private Vector3 direction;

    void Update()
    {
        if(time <= 0)
        {
            if (Input.GetButtonDown("Fire1") || autoShoot)
            {
                shotProjectile = Instantiate(projectile, 
                    transform.position, 
                    transform.rotation
                    );

                if (forceDirection)
                {
                    direction = Vector3.forward;
                }
                else
                {
                    direction = transform.forward;
                }

                shotProjectile.GetComponent<ProjectileBehaviour>().shotBy = tag;
                rb = shotProjectile.GetComponent<Rigidbody>();
                rb.AddForce(direction * speed);

                time = minShootingInterval;
            }
        }
        else
        {
            time -= Time.deltaTime;
        }

    }
}
