using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthpackBehaviour : MonoBehaviour
{
    public float healthValue;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            SoundController.sc.PlaySound("coin_04", transform.position, false);
            ObjectHealth playerHealth = col.gameObject.GetComponent<ObjectHealth>();
            playerHealth.currentHealth += healthValue;

            if (playerHealth.currentHealth > playerHealth.maxHealth)
            {
                playerHealth.maxHealth = playerHealth.currentHealth;
            }

            Destroy(gameObject);
        }
    }
}
