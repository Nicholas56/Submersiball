using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum AvailablePickupIcons { decreaseSize, Mine, Radar}

public class UI_Manager : MonoBehaviour
{
    [SerializeField] float startingTimeValue;
    float currentTimeValue;

    [SerializeField] SubmarineControl playerOne;

    [SerializeField] SubmarineControl playerTwo;

    [SerializeField] Slider speedSliderPlayerOne;

    [SerializeField] Slider speedSliderPlayerTwo;

    [SerializeField] Slider boostSliderPlayerOne;

    [SerializeField] Slider boostSliderPlayerTwo;

    [SerializeField] GameObject propellerPlayerOne;

    [SerializeField] GameObject propellerPlayerTwo;


    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] TextMeshProUGUI teamOneScoreText;

    [SerializeField] TextMeshProUGUI teamTwoScoreText;

    [SerializeField] Sprite mineIcon;

    [SerializeField] Sprite decreaseSizeIcon;

    [SerializeField] Sprite radarIcon;

    [SerializeField] GameObject currentPlayerOneIcon;

    [SerializeField] GameObject currentPlayerTwoIcon;

    [SerializeField] TextMeshProUGUI endGameTeamOneScore;
    [SerializeField] TextMeshProUGUI endGameTeamTwoScore;

    [SerializeField] TextMeshProUGUI endGameTeamOneWin;
    [SerializeField] TextMeshProUGUI endGameTeamTwoWin;

    [SerializeField] TextMeshProUGUI drawText;

    bool gameOver = false;

    int startingScore = 0;

    int teamOneScore;

    int teamTwoScore;

    public static UI_Manager current;

    GameObject playerOnePointer;

    GameObject playerTwoPointer;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        playerOnePointer = GameObject.FindGameObjectWithTag("Pointer1");

        playerTwoPointer = GameObject.FindGameObjectWithTag("Pointer2");

        SetInitialValues();
    }

    private void FixedUpdate()
    {
        UpdateSpeedBar();

        UpdateBoostBar();
    }

    void Update()
    {
        DisplayTime();

        UpdatePropeller();

        GameOver();
    }

    private void DisplayTime()
    {
        currentTimeValue -= Time.deltaTime;

        float minutes = Mathf.FloorToInt(currentTimeValue / 60);
        float seconds = Mathf.FloorToInt(currentTimeValue % 60);

        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void ScoreTeamOne()
    {
        teamOneScore += 1;
        teamOneScoreText.text = teamOneScore.ToString();
    }

    public void ScoreTeamTwo()
    {
        teamTwoScore += 1;
        teamTwoScoreText.text = teamTwoScore.ToString();
    }

    public void UpdateSpeedBar()
    {
        speedSliderPlayerOne.value = playerOne.currentSpeed;
        if (SinglePlayerSetup.current.style == SinglePlayerSetup.playstyle.Multiplayer)
            speedSliderPlayerTwo.value = playerTwo.currentSpeed;
    }

    public void UpdateBoostBar()
    {
        boostSliderPlayerOne.value = playerOne.currentBoost;
        if (SinglePlayerSetup.current.style == SinglePlayerSetup.playstyle.Multiplayer)
            boostSliderPlayerTwo.value = playerTwo.currentBoost;
    }

    public void SetTime(float value)
    {
        startingTimeValue = value;
    }

    public void SetInitialValues()
    {
        teamOneScore = startingScore;
        teamOneScoreText.text = teamOneScore.ToString();

        teamTwoScore = startingScore;
        teamTwoScoreText.text = teamTwoScore.ToString();

        currentTimeValue = startingTimeValue;
    }

    public void UpdatePropeller()
    {
        if (playerOne.GetComponent<SubmarineControl>().accelUI)
        {
            propellerPlayerOne.transform.Rotate(new Vector3(0f, 0f, 100f) * playerOne.currentSpeed / 800);
        }
        else
        {
            propellerPlayerOne.transform.Rotate(new Vector3(0f, 0f, 100f) * playerOne.currentSpeed / Mathf.Lerp(800, 2000, 3));
        }
        if (SinglePlayerSetup.current.style == SinglePlayerSetup.playstyle.Multiplayer)
        {
            if (playerTwo.GetComponent<SubmarineControl>().accelUI)
            {
                propellerPlayerTwo.transform.Rotate(new Vector3(0f, 0f, 100f) * playerTwo.currentSpeed / 800);
            }
            else
            {
                propellerPlayerTwo.transform.Rotate(new Vector3(0f, 0f, 100f) * playerTwo.currentSpeed / Mathf.Lerp(800, 2000, 3));
            }
        }
    }

    public void DisableRadarPlayerOne()
    {
        playerOnePointer.SetActive(false);

        StartCoroutine("PlayerOneRadarCoolDown");
    }

    IEnumerator PlayerOneRadarCoolDown()
    {
        yield return new WaitForSeconds(7);

        playerOnePointer.SetActive(true);
    }

    public void DisableRadarPlayerTwo()
    {
        playerTwoPointer.SetActive(false);

        StartCoroutine("PlayerTwoRadarCoolDown");
    }

    IEnumerator PlayerTwoRadarCoolDown()
    {
        yield return new WaitForSeconds(7);

        playerTwoPointer.SetActive(true);
    }

    public AvailablePickupIcons SetPlayerPickupIcon(AvailablePickupIcons icon, int playerNumber)
    {
        if (playerNumber == 1)
        {
            currentPlayerOneIcon.gameObject.SetActive(true);

            if (icon == AvailablePickupIcons.Radar)
            {
                currentPlayerOneIcon.GetComponent<Image>().sprite = radarIcon;
            }

            if (icon == AvailablePickupIcons.decreaseSize)
            {
                currentPlayerOneIcon.GetComponent<Image>().sprite = decreaseSizeIcon;
            }

            if (icon == AvailablePickupIcons.Mine)
            {
                currentPlayerOneIcon.GetComponent<Image>().sprite = mineIcon;
            }
        }

        if(playerNumber == 2)
        {
            currentPlayerTwoIcon.gameObject.SetActive(true);

            if (icon == AvailablePickupIcons.Radar)
            {
                currentPlayerTwoIcon.GetComponent<Image>().sprite = radarIcon;
            }

            if (icon == AvailablePickupIcons.decreaseSize)
            {
                currentPlayerTwoIcon.GetComponent<Image>().sprite = decreaseSizeIcon;
            }

            if (icon == AvailablePickupIcons.Mine)
            {
                currentPlayerTwoIcon.GetComponent<Image>().sprite = mineIcon;
            }
        }

        return icon;
    }

    public void ClearPlayerIcon(int playerNumber)
    {
        if (playerNumber == 1)
        {
            currentPlayerOneIcon.GetComponent<Image>().sprite = null;

            currentPlayerOneIcon.gameObject.SetActive(false);
        }

        if (playerNumber == 2)
        {
            currentPlayerTwoIcon.GetComponent<Image>().sprite = null;

            currentPlayerTwoIcon.gameObject.SetActive(false);
        }
    }

    public void ResetUI()
    {
        gameOver = false;
        currentTimeValue = 1;

        endGameTeamOneWin.gameObject.SetActive(false);
        endGameTeamTwoWin.gameObject.SetActive(false);
        drawText.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        if(currentTimeValue <= 0 && gameOver == false)
        {
            gameOver = true;

            SinglePlayerSetup.current.GameOver();


            endGameTeamOneScore.text = teamOneScore.ToString();
            endGameTeamTwoScore.text = teamTwoScore.ToString();

            if(teamOneScore > teamTwoScore)
            {
                endGameTeamOneWin.gameObject.SetActive(true);
            }
            
            if(teamTwoScore > teamOneScore)
            {
                endGameTeamTwoWin.gameObject.SetActive(true);
            }

            if(teamTwoScore == teamOneScore)
            {
                drawText.gameObject.SetActive(true);
            }
        }
    }
}