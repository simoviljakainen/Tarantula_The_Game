using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float maxAngle;
    public float maxSpeed;
    public float minSpeed;
    public float damage = 10f;
    public float direction;
    public float turnTimer = 0.05f;

    private float speed, angle;
    private float timer = 0f;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed) * GameService.gameDifficulty;
        angle = Random.Range(-maxAngle, maxAngle);
        timer = turnTimer;
        transform.eulerAngles = new Vector3(0f, direction + angle, 0f);
    }

    void FixedUpdate()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        transform.Translate(transform.forward * speed * -1, relativeTo: Space.World);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Border") && timer < 0)
        {
            transform.eulerAngles *= -1;
            timer = turnTimer;
        }

    }

}
