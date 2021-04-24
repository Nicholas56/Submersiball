using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//Attached to the player body to allow left right looking
public class LookX : MonoBehaviour
{
    [SerializeField] float sensitivity = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Hides and limits the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Locks looking if the option menu is open
        //if (!OptionScript.isPaused)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
