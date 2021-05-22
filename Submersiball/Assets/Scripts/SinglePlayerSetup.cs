using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerSetup : MonoBehaviour
{
    public static SinglePlayerSetup current;
    public bool inPlay = false;

    [Header("Screen Options")]
    [SerializeField] GameObject setupPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject guiPanel;

    [Header("Map Options")]
    [SerializeField] List<GameObject> maps;
    [SerializeField] GameObject lights;
    [SerializeField] GameObject blackout;
    [SerializeField] GameObject mines;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject playerSub;
    [SerializeField] GameObject playerSub2;
    [SerializeField] GameObject goalie1;
    [SerializeField] GameObject goalie2;
    [SerializeField] List<GameObject> AISubs;
    GameObject currentMap;

    [Header("AI Options")]
    [SerializeField] int maxAI = 3;
    [SerializeField] int numOfAI = 0;
    [SerializeField] bool goalie1Aim;
    [SerializeField] bool goalie2Aim;
    int AISpeed = 4;
    [SerializeField] int easyAISpeed;
    [SerializeField] int midAISpeed;
    [SerializeField] int hardAISpeed;

    [Header("Team Options")]
    [SerializeField] [Range(1, 2)] int playerTeam = 1;
    [SerializeField] Image team1Color;
    [SerializeField] Image team2Color;
    [SerializeField] Material team1Material;
    [SerializeField] Material team2Material;
    [SerializeField] Material team1Arena;
    [SerializeField] Material team2Arena;
    public enum playstyle { Singleplayer, Multiplayer}
    public playstyle style = playstyle.Singleplayer;

    [Header("Position Options")]
    [SerializeField] List<Transform> playerPositions;
    List<Transform> previousSpawns;
    private void Awake()
    {
        current = this;
        UnlockCursor();
        currentMap = maps[0];
        Goalie1Choice(0);Goalie2Choice(0);
    }
    private void Start()
    {
        ChangeTeam1Color(GameManager.current.team1Color);
        ChangeTeam2Color(GameManager.current.team2Color); 
        TimeChoice(0);DifficultyChoice(0);ModeChoice(0);

        GameEvents.current.onChangeTeam1Color += ChangeTeam1Color;
        GameEvents.current.onChangeTeam2Color += ChangeTeam2Color;
    }
    private void OnDestroy()
    {
        GameEvents.current.onChangeTeam1Color -= ChangeTeam1Color;
        GameEvents.current.onChangeTeam2Color -= ChangeTeam2Color;
    }
    public void BeginGame()
    {
        UnPause();
        setupPanel.SetActive(false);
        PlacePlayers();
        ball.transform.position = Vector3.zero;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

        UI_Manager.current.SetInitialValues();
        GameEvents.current.StartMatch();
    }

    public void MapChoice(int mapNum)
    {
        currentMap.SetActive(false);
        maps[mapNum].SetActive(true);
        currentMap = maps[mapNum];
        GameEvents.current.PressButton();
    }
    public void PlayerNumChoice(int playerNum) { numOfAI = Mathf.Min(playerNum,maxAI); GameEvents.current.PressButton();    }
    public void Goalie1Choice(int choice) 
    {
        switch (choice)
        {
            case 0: goalie1.SetActive(false);goalie1Aim = false; break;
            case 1: goalie1.SetActive(true); goalie1Aim = false; break;
            case 2: goalie1.SetActive(true); goalie1Aim = true; break;
        }
        GameEvents.current.PressButton();
    }
    public void Goalie2Choice(int choice) 
    {
        switch (choice)
        {
            case 0: goalie2.SetActive(false); goalie2Aim = false; break;
            case 1: goalie2.SetActive(true); goalie2Aim = false; break;
            case 2: goalie2.SetActive(true); goalie2Aim = true; break;
        }
        GameEvents.current.PressButton();
    }
    public void DifficultyChoice(int choice)
    {
        switch (choice)
        {
            case 0:AISpeed = easyAISpeed; break;
            case 1:AISpeed = midAISpeed; break;
            case 2:AISpeed = hardAISpeed; break;
        }
        GameEvents.current.PressButton();
    }
    public void ModeChoice(int choice)
    {
        lights.SetActive(true);mines.SetActive(false);blackout.SetActive(false);
        switch (choice)
        {
            case 1:mines.SetActive(true); break;
            case 2:lights.SetActive(false);blackout.SetActive(true); break;
            case 3:mines.SetActive(true);lights.SetActive(false); break;
        }
        GameEvents.current.PressButton();
    }
    public void TimeChoice(int choice)
    {
        switch (choice)
        {
            case 0: UI_Manager.current.SetTime(60f); break;
            case 1: UI_Manager.current.SetTime(90f); break;
            case 2: UI_Manager.current.SetTime(120f); break;
            case 3: UI_Manager.current.SetTime(180f); break;
            case 4: UI_Manager.current.SetTime(240f); break;
            case 5: UI_Manager.current.SetTime(300f); break;
        }
        GameEvents.current.PressButton();
    }
    public void ChangeTeam1Color(Color color)
    {
        if (team2Color.color == color) { return; }
        team1Color.color = color;
        team1Material.color = color;
        team1Arena.color = color;
        GameManager.current.team1Color = color;
        GameEvents.current.PressButton();
    }
    public void ChangeTeam2Color(Color color)
    {
        if (team1Color.color == color) { return; }
        team2Color.color = color;
        team2Material.color = color;
        team2Arena.color = color;
        GameManager.current.team2Color = color;
        GameEvents.current.PressButton();
    }
    void UnlockCursor()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void LockCursor()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void PlacePlayers()
    {
        HideSubs();

        previousSpawns = new List<Transform>();
        int num = Random.Range(0, playerPositions.Count);
        playerSub.transform.position = playerPositions[num].position;
        playerSub.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerSub.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        previousSpawns.Add(playerPositions[num]);

        if (style==playstyle.Multiplayer)
        {
            num = (num+1)%playerPositions.Count;
            playerSub2.transform.position = playerPositions[num].position;
            playerSub2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            previousSpawns.Add(playerPositions[num]);
        }

        for (int i = 0; i < (numOfAI); i++)
        {
            num = Random.Range(0, playerPositions.Count);
            if (previousSpawns.Contains(playerPositions[num])) { i--;continue; }
            else
            {
                AISubs[i].SetActive(true);
                AISubs[i].transform.position = playerPositions[num].position;
                previousSpawns.Add(playerPositions[num]);

                AISubs[i].GetComponent<AIAttacker>().team = i % 2 == 0 ? 2 : 1;
                AISubs[i].GetComponent<AIAttacker>().TeamColors();
                AISubs[i].GetComponent<AIAttacker>().moveSpeed = AISpeed;
                AISubs[i].GetComponent<Explode>().respawnPoint = playerPositions[num].position;
            }
        }
        goalie1.GetComponentInChildren<AIGoalKeeper>().aim = goalie1Aim;
        goalie2.GetComponentInChildren<AIGoalKeeper>().aim = goalie2Aim;
    }
    void HideSubs()
    {
        for (int j = 0; j < AISubs.Count; j++)
        {
            AISubs[j].SetActive(false);
        }
    }
    public void EndSession()
    {
        PlacePlayers();
        UnlockCursor();
        setupPanel.SetActive(true);

        HideSubs();
        inPlay = false;
        GameEvents.current.EndMatch();
        GameEvents.current.PressButton();
    }
    public void Pause()
    {
        UnlockCursor();
        pausePanel.SetActive(true);
        guiPanel.SetActive(false);
        inPlay = false;
        GameEvents.current.PressButton();
    }
    public void UnPause()
    {
        LockCursor();
        pausePanel.SetActive(false);
        guiPanel.SetActive(true);
        inPlay = true;
        GameEvents.current.PressButton();
    }
}
