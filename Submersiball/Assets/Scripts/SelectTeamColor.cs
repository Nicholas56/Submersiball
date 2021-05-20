using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectTeamColor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image colorChoice;
    [SerializeField][Range(1,2)] int teamNum = 1;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (teamNum == 1) 
        { 
            GameEvents.current.ChangeTeam1Color(colorChoice.color);
        }
        else 
        {
            GameEvents.current.ChangeTeam2Color(colorChoice.color);
        }
    }
}
