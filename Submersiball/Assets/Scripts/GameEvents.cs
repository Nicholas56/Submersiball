using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents 
{
    static GameEvents _current;
    public static GameEvents current
    {
        get
        {
            if (_current == null)
            {
                _current = new GameEvents();
            }
            return _current;
        }
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    public event Action onPressButton;
    public void PressButton() { if (onPressButton != null) { onPressButton(); } }
    public event Action<bool> onAccelerate;
    public void Accelerate(bool accel) { if (onAccelerate != null) { onAccelerate(accel); } }
    public event Action<bool> onBoost;
    public void Boost(bool boost) { if (onBoost != null) { onBoost(boost); } }
    public event Action onExplode;
    public void Explode() { if (onExplode != null) { onExplode(); } }
    public event Action onMineExplode;
    public void MineExplode() { if (onMineExplode != null) { onMineExplode(); } }
    public event Action onScoreGoal;
    public void ScoreGoal() { if (onScoreGoal != null) { onScoreGoal(); } }
    public event Action onStartMatch;
    public void StartMatch() { if (onStartMatch != null) { onStartMatch(); } }
    public event Action onEndMatch;
    public void EndMatch() { if (onEndMatch != null) { onEndMatch(); } }
    public event Action onLastTenSeconds;
    public void LastTenSeconds() { if (onLastTenSeconds != null) { onLastTenSeconds(); } }
    public event Action<Color> onChangeTeam1Color;
    public void ChangeTeam1Color(Color color) { if (onChangeTeam1Color != null) { onChangeTeam1Color(color); } }
    public event Action<Color> onChangeTeam2Color;
    public void ChangeTeam2Color(Color color) { if (onChangeTeam2Color != null) { onChangeTeam2Color(color); } }

}
