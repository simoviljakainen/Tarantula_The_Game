using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public float invulnTime = 3f;

    public bool InflictDamage(GameObject obj, float damage)
    {
        ObjectHealth objHealth = obj.GetComponent<ObjectHealth>();

        if (!objHealth.isVulnerable)
        {
            return false;
        }

        if((objHealth.currentHealth -= damage) <= 0 && !obj.CompareTag("Player"))
        {
            return true;
        }

        if(obj.CompareTag("Player"))
        {
            StartCoroutine(InvulnerabilityTimer(invulnTime, objHealth));
        }

        return false;
    }

    public static void InstantDamage(GameObject obj, float damage)
    {
        obj.GetComponent <ObjectHealth>().currentHealth -= damage;
    }

    IEnumerator InvulnerabilityTimer(float time, ObjectHealth obj)
    {
        obj.isVulnerable = false;
        yield return new WaitForSeconds(time);
        obj.isVulnerable = true;
    }
}
