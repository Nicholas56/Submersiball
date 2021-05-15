using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Material team1Mat;
    public Material team2Mat;
    public Color team1Color;
    public Color team2Color;
    public TMP_FontAsset gameFont;

    public static GameManager current;

    private void Awake()
    {
        current = this;
        team1Color = team1Mat.color;
        team2Color = team2Mat.color;
    }
}
