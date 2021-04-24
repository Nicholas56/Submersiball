using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform rotatePoint;
    Vector3 mousePos;
    Quaternion initialRotation;
    Vector3 initialPosition;

    [SerializeField] float xSensitivity = 0.1f;
    [SerializeField] float ySensitivity = 0.1f;
    [SerializeField] bool freeCamera = false;
    [SerializeField] KeyCode freeCameraButton;

    private void Start()
    {
        rotatePoint = transform.parent;
        mousePos = Input.mousePosition;
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        //Key to toggle free camera
        if (Input.GetKeyDown(freeCameraButton)) { freeCamera = !freeCamera;if (!freeCamera) { transform.localRotation = initialRotation;transform.localPosition = initialPosition; } }
    }

    private void FixedUpdate()
    {
        if (freeCamera)
        {
            Vector3 deltaPos = mousePos - Input.mousePosition;

            mousePos = Input.mousePosition;

            transform.RotateAround(rotatePoint.position, transform.up, deltaPos.x * xSensitivity);
            transform.RotateAround(rotatePoint.position, transform.right, deltaPos.y * ySensitivity);
        }
    }
}
