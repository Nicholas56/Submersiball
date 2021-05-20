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

        GameEvents.current.onStartMatch += SetColor;
        
        item = gameObject.GetComponent<TMP_Text>();
        item.font = GameManager.current.gameFont;
        switch (type)
        {
            case TextType.None: break;
            case TextType.Team1:
                item.color = GameManager.current.team1Color;
                GameEvents.current.onChangeTeam1Color += ChangeTextColor;
                break;
            case TextType.Team2:
                item.color = GameManager.current.team2Color;
                GameEvents.current.onChangeTeam2Color += ChangeTextColor;
                break;
        }
    }
    private void OnDestroy()
    {
        GameEvents.current.onStartMatch -= SetColor;
        switch (type)
        {
            case TextType.None: break;
            case TextType.Team1:
                GameEvents.current.onChangeTeam1Color -= ChangeTextColor;
                break;
            case TextType.Team2:
                GameEvents.current.onChangeTeam2Color -= ChangeTextColor;
                break;
        }

    }

    void ChangeTextColor(Color color) { item.color = color; }
    void SetColor()//The score numbers still use the wrong color when starting a new game after changing team colors.
    {switch (type)
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
