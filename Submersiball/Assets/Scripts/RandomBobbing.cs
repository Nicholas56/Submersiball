using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBobbing : MonoBehaviour
{
    float bobRange = 0;
    float bobSpeed = 0.2f;
    Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
        bobRange = Random.Range(0.5f, 10f);
    }

    private void Update()
    {
        if (transform.position.y > initialPos.y + bobRange)
        {
            bobSpeed = -bobSpeed;
        }
        else if (transform.position.y < initialPos.y - bobRange)
        {
            bobSpeed = -bobSpeed;
        }
        transform.position = new Vector3(initialPos.x, transform.position.y + bobSpeed * Time.deltaTime, initialPos.z);
    }
}
