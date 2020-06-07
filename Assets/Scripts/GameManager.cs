using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuScreens { Main, Highscore }

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public static int GameplayLength = 50;

    public string playerName { get; private set; } = "SAM";
    public int gameScore { get; private set; }
    public bool scoreLogged { get; private set; } = true;

    public MenuScreens menuDisplay { get; private set; } = MenuScreens.Main;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {

    }

    public void EndGame(int score)
    {
        gameScore = score;
        menuDisplay = MenuScreens.Highscore;
        scoreLogged = false;
        SceneManager.LoadScene(0);
    }

    public void SetName(string name)
    {
        playerName = name;
    }

    public void LogScore(HighscoreManager highscoreManager)
    {
        if (scoreLogged == true) return;

        highscoreManager.AddScore(gameScore, playerName);
        scoreLogged = true;
    }
}
