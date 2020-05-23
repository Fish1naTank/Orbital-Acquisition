using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
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
}
