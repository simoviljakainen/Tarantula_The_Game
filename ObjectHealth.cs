using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public GameObject healthBar;
    public float currentHealth = 100, maxHealth = 100;
    public bool isVulnerable = true;

    private GameObject healthBarInstance;
    private float healthBefore, healthRatio;
    private float initialScaleX;
    private MeshRenderer mRenderer;
    private Vector3 initialPos;
    private Quaternion initialQuart;

    void Start()
    {
        healthBarInstance = Instantiate(healthBar);
        mRenderer = healthBarInstance.GetComponent<MeshRenderer>();
        healthBarInstance.transform.parent = gameObject.transform;

        healthBefore = currentHealth;
        initialScaleX = healthBarInstance.transform.localScale.x;

        mRenderer.enabled = false;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {

            SoundController.sc.PlaySound("hit_01", transform.position, false);

            if (CompareTag("Player"))
            {
                gameObject.SetActive(false);
                UIController.uiInstance.PlayerDeath();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (currentHealth != healthBefore)
        {
            SoundController.sc.PlaySound("explosion_20", transform.position, false);

            StopCoroutine("HideHealthBar");
            mRenderer.enabled = true;

            healthRatio = currentHealth / maxHealth;

            /* Resize and recolor the healthbar */
            mRenderer.material.color = Color.Lerp(Color.red, Color.green, healthRatio);
            
            healthBarInstance.transform.localScale = new Vector3(
                initialScaleX * healthRatio,
                healthBarInstance.transform.localScale.y,
                healthBarInstance.transform.localScale.z
                );

            healthBefore = currentHealth;
            StartCoroutine("HideHealthBar");
        }

        /* Move and orientate the healthbar */
        healthBarInstance.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 2,
            transform.position.z);
        
    }

    IEnumerator HideHealthBar()
    {
        yield return new WaitForSeconds(2f);
        mRenderer.enabled = false;
    }

}
