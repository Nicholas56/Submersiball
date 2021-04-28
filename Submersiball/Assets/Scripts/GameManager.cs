using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Material team1Mat;
    public Material team2Mat;

    public static GameManager current;

    private void Awake()
    {
        current = this;
    }
}
