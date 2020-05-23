using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Text difficultyText;

    void Start()
    {
        updateDifficultyText();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            if (sceneToLoad > SceneManager.sceneCountInBuildSettings - 1) sceneToLoad = 0;

            SceneManager.LoadScene(sceneToLoad);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int sceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
            if (sceneToLoad < 0) sceneToLoad = SceneManager.sceneCountInBuildSettings - 1;

            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void updateDifficultyText()
    {
        difficultyText.text = "difficulty : " + SceneManager.GetActiveScene().name;
    }
}
