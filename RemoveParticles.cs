using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveParticles : MonoBehaviour
{
    private ParticleSystem pSystem;

    void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
        pSystem.Play();        
    }

    void Update()
    {
        if (!pSystem.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
