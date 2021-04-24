using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//Attached to the camera to allow up down looking
public class LookY : MonoBehaviour
{
    [SerializeField] float sensitivityY = 4F;
    public float minimumY = -30F;
    public float maximumY = 30F;
    float rotationY = 0F;

    //Returns the private variable rotationY in the event another script requires it
    public float GetRotation()
    {
        return rotationY;
    }

    // Update is called once per frame
    void Update()
    {
        //Locks looking if the option menu is open
        //if (!OptionScript.isPaused)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }
}
