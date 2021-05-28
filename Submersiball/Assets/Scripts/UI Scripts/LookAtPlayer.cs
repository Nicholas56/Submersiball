using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    GameObject player1;

    GameObject player2;

    [SerializeField] int playerNumber;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }

    void Update()
    {
        if(player2)
        LookAt(playerNumber);
    }


    void LookAt(int playerNum)
    {
        if(playerNum == 1)
        {
            transform.LookAt(player2.transform.position);
        }

        if(playerNum == 2)
        {
            transform.LookAt(player1.transform.position);
        }
    }
}
