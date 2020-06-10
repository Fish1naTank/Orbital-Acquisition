using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerScore : MonoBehaviour
{
    public Text addScoreText;
    public UITweener addScoreAnimator;

    public Image timerImage;
    private RectTransform timerImageTransform;

    public int runningScore = 100;

    public int score { get; private set; }

    private Text scoreText;
    private bool gameStart = false;

    public float elapsedTime;
    private float secondClock;

    void Awake()
    {
        scoreText = GetComponent<Text>();

        timerImageTransform = timerImage.rectTransform;
        setTimerImageColor(GameManager.GameplayLength);
        //timerImageTransform.sizeDelta = new Vector2(timerImageTransform.sizeDelta.x, GameManager.GameplayLength);
    }

    void FixedUpdate()
    {
        if(gameStart == true)
        {
            secondClock += Time.deltaTime;
            while(secondClock > 1f)
            {
                secondClock -= 1f;
                AddScore(runningScore);
            }

            elapsedTime += Time.deltaTime;

            if(elapsedTime > GameManager.GameplayLength)
            {
                gameStart = false;
                GameManager.instance.EndGame(score);
            }

            float timeLeft = GameManager.GameplayLength - elapsedTime;
            timerImageTransform.sizeDelta = new Vector2(timerImageTransform.sizeDelta.x, timeLeft);

            setTimerImageColor(timeLeft);
        }
    }

    public void AddScore(int points)
    {
        score += points;

        if (Math.Abs(points) > runningScore)
        {
            UpdateAddScoreText(points);
        }

        UpdateScoreText();
    }

    public void StartGame()
    {
        elapsedTime = 0;
        gameStart = true;
    }

    private void UpdateAddScoreText(int points)
    {
        string pointsStr = points.ToString();
        if (points > 0)
        {
            pointsStr = "+" + points.ToString();
            addScoreText.color = Color.green;
        }
        else
        {
            pointsStr = points.ToString();
            addScoreText.color = Color.red;
        }

        addScoreText.text = pointsStr;
        addScoreAnimator.Show();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    private void setTimerImageColor(float timeLeft)
    {
        timerImage.color = Color.Lerp(Color.red, Color.green, timeLeft / GameManager.GameplayLength);
    }
}
