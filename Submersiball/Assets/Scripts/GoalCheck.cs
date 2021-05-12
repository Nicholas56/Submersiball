﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;

    UI_Manager uiMan;

    private void Start()
    {
        uiMan = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UI_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            other.transform.position = Vector3.zero;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ps.Play();

            if(tag == "GoalLeft")
            {
                ScoreLeft();
            }
            else if (tag == "GoalRight")
            {
                ScoreRight();
            }
        }
    }

    private void ScoreLeft()
    {
        uiMan.ScoreTeamOne();
    }

    private void ScoreRight()
    {
        uiMan.ScoreTeamTwo();

    }
}
