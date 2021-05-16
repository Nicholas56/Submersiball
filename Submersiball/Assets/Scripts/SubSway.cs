using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSway : MonoBehaviour
{
    public float swayAmount;
    public float maxAmount;
    public float smoothAmount;

    private Vector3 initalPosition;

    private void Start()
    {
        initalPosition = transform.localPosition;
    }

    private void Update()
    {
        float movementX = -transform.rotation.x * swayAmount / 100; //Get mouse input x axis
        float movementY = -transform.rotation.y * swayAmount / 100; //Get mouse input y axis

        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount); //clamp the sub movement 
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount); //clamp the sub movement 

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initalPosition, Time.deltaTime * smoothAmount); //Lerp weapon movement
    }
}
