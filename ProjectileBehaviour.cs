using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float maxDistance = 100f;
    public float damage = 20;
    public string shotBy;
    public GameObject hitParticles;

    private Vector3 spawnPoint;
    private DamageSystem damageSys;
    private bool isdead;

    void Start()
    {
        spawnPoint = transform.position;
        damageSys = FindObjectOfType<DamageSystem>();
        SoundController.sc.PlaySound("Hand Gun 1", transform.position, false);

    }

    void FixedUpdate()
    {
        /* Remove projectile */
        if (Vector3.Distance(spawnPoint, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag(shotBy) && col.gameObject.GetComponent<ObjectHealth>() != null)
        {
            Renderer objRenderer = col.gameObject.GetComponentInChildren<Renderer>();
            GameObject particles = Instantiate(hitParticles, transform.position, new Quaternion());

            particles.GetComponent<Renderer>().material = objRenderer.material;

            isdead = damageSys.InflictDamage(col.gameObject, damage);

            /* If player kills enemy */
            if (col.gameObject.CompareTag("Enemy") && shotBy == "Player" && isdead)
            {
                GameService.kills++;
                UIController.uiInstance.SetKillCount(GameService.kills);
            }

            Destroy(gameObject);
        }
    }

}
