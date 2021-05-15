using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ManageFont : MonoBehaviour
{
    public enum TextType { None, Team1,Team2}
    [SerializeField] TextType type = TextType.None;
    TMP_Text item;

    private void Start()
    {
        if (gameObject.GetComponent<TMP_Text>() == null) { return; }

        item = gameObject.GetComponent<TMP_Text>();
        item.font = GameManager.current.gameFont;
        switch (type)
        {
            case TextType.None: break;
            case TextType.Team1:
                item.color = GameManager.current.team1Color;
                break;
            case TextType.Team2:
                item.color = GameManager.current.team2Color;
                break;
        }
    }
}
