using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public Transform backPlatform;
    public Transform player;
    public float cameraOffsetPercent;

    private float platformLength;
    private Transform secondPlatform, firstPlatform;

    void Start()
    {
        firstPlatform = transform;
        secondPlatform = backPlatform;
        platformLength = GetComponent<Renderer>().bounds.size.z;
    }

    void Update()
    {
        /* Move a platform from back to front, when player has passes it  */
        if (player.position.z - platformLength * cameraOffsetPercent > firstPlatform.position.z - platformLength / 2)
        {
            MovePlatform(secondPlatform, firstPlatform);

            Transform temp = firstPlatform;
            firstPlatform = secondPlatform;
            secondPlatform = temp;
        }
    }

    void MovePlatform(Transform newFrontPlatform, Transform newBackPlatform)
    {
        newFrontPlatform.position = new Vector3(
            newBackPlatform.position.x,
            newBackPlatform.position.y,
            newBackPlatform.position.z + platformLength
            );

    }
}
