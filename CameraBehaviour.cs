using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;

    Transform interestPoint = null;

    void Update()
    {
        if(interestPoint != null)
        {
            transform.LookAt(interestPoint);
        }
        else
        {
            transform.position = target.position + cameraOffset;
        }
    }

    public void FollowTransform(Transform t)
    {
        interestPoint = t;
    }

}
