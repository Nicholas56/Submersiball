using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField]Transform rotatePoint;
    [SerializeField]Transform lookAtPoint;
    [SerializeField]float lookDistance = 10;
    Vector3 lookAtVector;
    bool lookAtBall;


    private void Start()
    {
    }

    private void FixedUpdate()
    {
        transform.position = rotatePoint.position - (lookDistance * FindVectorToTarget(lookAtPoint, rotatePoint));
        transform.LookAt(lookAtPoint);
    }

    Vector3 FindVectorToTarget(Transform target, Transform rotateAround)
    {
        lookAtVector = (lookAtPoint.position - rotateAround.position).normalized;
        return lookAtVector;
    }
}
