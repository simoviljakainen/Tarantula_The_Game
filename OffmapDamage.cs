using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffmapDamage : MonoBehaviour
{
    void FixedUpdate()
    {
        if (transform.position.y < 0)
        {
            DamageSystem.InstantDamage(gameObject, 1);
        }
    }
}
