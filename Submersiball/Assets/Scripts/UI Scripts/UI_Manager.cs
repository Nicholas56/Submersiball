using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] float startingTimeValue;
    float currentTimeValue;

    [SerializeField] Slider speedSlider;

    [SerializeField] Slider boostSlider;

    SubmarineControl subMovement;

    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] GameObject propeller;

    [SerializeField] GameObject ballPointer;

    [SerializeField] TextMeshProUGUI teamOneScoreText;

    [SerializeField] TextMeshProUGUI teamTwoScoreText;

    int startingScore = 0;

    int teamOneScore;

    int teamTwoScore;

    [SerializeField] GameObject ball;

    public static UI_Manager current;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        subMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<SubmarineControl>();

        SetInitialValues();
    }

    private void FixedUpdate()
    {
        UpdateSpeedBar();

        UpdateBoostBar();

        UpdateBallPointer();
    }

    void Update()
    {
        DisplayTime();

        propeller.transform.Rotate(new Vector3(0f, 0f, 100f) * subMovement.currentSpeed / 500);
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
        speedSlider.value = subMovement.currentSpeed;
    }

    public void UpdateBoostBar()
    {
        boostSlider.value = subMovement.currentBoost;
    }

    public void UpdateBallPointer()
    {
        Vector3 targetPosition = ball.transform.position;
        ballPointer.transform.LookAt(targetPosition);
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
}