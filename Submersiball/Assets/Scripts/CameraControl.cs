using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform rotatePoint;
    Quaternion initialRotation;
    Vector3 initialPosition;

    Vector3 deltaPos;

    [SerializeField] float xSensitivity = 0.1f;
    [SerializeField] float ySensitivity = 0.1f;

    private void Start()
    {
        rotatePoint = transform.parent;
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        transform.RotateAround(rotatePoint.position, transform.up, deltaPos.x * xSensitivity);
        transform.RotateAround(rotatePoint.position, transform.right, deltaPos.y * ySensitivity);
    }

    public void Revert()
    {
        transform.localRotation = initialRotation; 
        transform.localPosition = initialPosition;
    }
    public void SetDeltaPos(Vector2 newDelta) { deltaPos = newDelta; }
}
