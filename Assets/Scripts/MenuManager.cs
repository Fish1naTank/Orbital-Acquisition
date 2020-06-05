using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menuDisplays;

    public HighscoreManager highscoreManager;
    public HighscoreTable highscoreTable;

    void Start()
    {
        int menuToDisplay = (int)GameManager.instance.menuDisplay;
        for (int i = 0; i < menuDisplays.Length; i++)
        {
            if (i == menuToDisplay)
            {
                menuDisplays[i].SetActive(true);
            }
            else
            {
                menuDisplays[i].SetActive(false);
            }
        }

        GameManager.instance.LogScore(highscoreManager);
        highscoreTable.UpdateTable();
    }
}
