using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLineBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject player;

    private Camera c;
    private float currentSpeed;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();

        c = Camera.main;
    }

    void FixedUpdate()
    {
        currentSpeed = speed + Mathf.Log(GameService.gameDifficulty + 1);

        if(currentSpeed > playerMovement.speed)
        {
            currentSpeed = playerMovement.speed;
        }

        transform.Translate(new Vector3(0, 0, currentSpeed));
        //Debug.Log("Speed: " + speed + currentSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectHealth oHealth;
        if ((oHealth = collision.gameObject.GetComponent<ObjectHealth>()) != null)
        {
            oHealth.currentHealth = 0;

            if (collision.gameObject.CompareTag("Player"))
            {
                c.GetComponent<CameraBehaviour>().FollowTransform(transform);
            }
        }
    }
}
