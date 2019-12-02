using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, jumpForce;
    public Rigidbody player;

    private float verticalForce, horizontalForce;
    private string ground, enemy;
    private Vector3 playerPos;
    private bool grounded;
    private DamageSystem damageSys;
    private Animator anim;

    void Start()
    {
        speed = 0.5f;
        ground = "Ground";
        enemy = "Enemy";

        damageSys = FindObjectOfType<DamageSystem>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            player.freezeRotation = true;
            player.AddForce(transform.up * jumpForce);
            grounded = false;

            anim.StopPlayback();
        }
    }

    void FixedUpdate()
    {
        playerPos = transform.position;

        verticalForce = Input.GetAxis("Vertical") * speed;
        horizontalForce = Input.GetAxis("Horizontal") * speed;

        Debug.Log(verticalForce);

        if(verticalForce > 0.1f && grounded)
        {
            anim.Play("Rotate");
        }
        else
        {
            anim.Play("Idle");
        }

        transform.position = new Vector3(
            playerPos.x + horizontalForce, 
            player.position.y, 
            playerPos.z + verticalForce
            );
    }

    /* Check if player is grounded */
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(ground))
        {
            grounded = true;
        }
        else if(col.gameObject.CompareTag(enemy))
        {
            damageSys.InflictDamage(
                gameObject,
                col.gameObject.GetComponent<EnemyBehaviour>().damage
                );
        }
    }

}
