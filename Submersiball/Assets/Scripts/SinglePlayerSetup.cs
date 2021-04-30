using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerSetup : MonoBehaviour
{
    [Header("Screen Options")]
    [SerializeField] KeyCode pauseKey;
    [SerializeField] GameObject setupPanel;

    [Header("Map Options")]
    [SerializeField] List<GameObject> maps;
    [SerializeField] GameObject lights;
    [SerializeField] GameObject mines;
    [SerializeField] GameObject playerSub;
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

    [Header("Position Options")]
    [SerializeField] List<Transform> playerPositions;
    List<Transform> previousSpawns;
    private void Start()
    {
        Time.timeScale = 0;
        currentMap = maps[0];
    }
    
    public void BeginGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        setupPanel.SetActive(false);
        PlacePlayers();
    }

    public void MapChoice(int mapNum)
    {
        currentMap.SetActive(false);
        maps[mapNum].SetActive(true);
        currentMap = maps[mapNum];
    }
    public void PlayerNumChoice(int playerNum) { numOfAI = Mathf.Min(playerNum,maxAI); }
    public void Goalie1Choice(int choice) 
    {
        switch (choice)
        {
            case 0: goalie1.SetActive(false);goalie1Aim = false; break;
            case 1: goalie1.SetActive(true); goalie1Aim = false; break;
            case 2: goalie1.SetActive(true); goalie1Aim = true; break;
        }
    }
    public void Goalie2Choice(int choice) 
    {
        switch (choice)
        {
            case 0: goalie2.SetActive(false); goalie2Aim = false; break;
            case 1: goalie2.SetActive(true); goalie2Aim = false; break;
            case 2: goalie2.SetActive(true); goalie2Aim = true; break;
        }
    }
    public void DifficultyChoice(int choice)
    {
        switch (choice)
        {
            case 0:AISpeed = easyAISpeed; break;
            case 1:AISpeed = midAISpeed; break;
            case 2:AISpeed = hardAISpeed; break;
        }
    }
    public void ModeChoice(int choice)
    {
        lights.SetActive(true);mines.SetActive(false);
        switch (choice)
        {
            case 1:mines.SetActive(true); break;
            case 2:lights.SetActive(false); break;
            case 3:mines.SetActive(true);lights.SetActive(false); break;
        }
    }

    void PlacePlayers()
    {
        for (int j = 0; j < AISubs.Count; j++)
        {
            AISubs[j].SetActive(false);
        }

        previousSpawns = new List<Transform>();
        int num = Random.Range(0, playerPositions.Count);
        playerSub.transform.position = playerPositions[num].position;
        previousSpawns.Add(playerPositions[num]);
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
}
