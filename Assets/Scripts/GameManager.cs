using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuScreens { Main, Highscore }
public enum GameDifficulty { Easy, Normal }

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public static int GameplayLength = 150;

    public GameDifficulty difficulty { get; private set; } = GameDifficulty.Easy;

    public bool characterGirl { get; private set; } = true;
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

    public void ReturnToMenu()
    {
        menuDisplay = MenuScreens.Main;
        SceneManager.LoadScene(0);
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

    public void SetCharacter(bool isGirl)
    {
        characterGirl = isGirl;
    }

    public void SetDifficulty(int newDifficulty)
    {
        switch (newDifficulty)
        {
            case 0:
                difficulty = GameDifficulty.Easy;
                break;
            case 1:
                difficulty = GameDifficulty.Normal;
                break;
        }
    }

    public void LogScore(HighscoreManager highscoreManager)
    {
        if (scoreLogged == true) return;

        highscoreManager.AddScore(gameScore, playerName);
        scoreLogged = true;
    }
}
