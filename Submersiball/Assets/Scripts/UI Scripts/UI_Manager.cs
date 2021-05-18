using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    int startingScore = 0;

    int teamOneScore;

    int teamTwoScore;

    public static UI_Manager current;    

    void Awake()
    {
        current = this;
    }

    void Start()
    {

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

        speedSliderPlayerTwo.value = playerTwo.currentSpeed;
    }

    public void UpdateBoostBar()
    {
        boostSliderPlayerOne.value = playerOne.currentBoost;

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