using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemaManager))]
public class MenuManager : MonoBehaviour
{
    public GameObject[] menuDisplays;

    public HighscoreManager highscoreManager;
    public HighscoreTable highscoreTable;

    private CinemaManager videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<CinemaManager>();
    }

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

        if (menuToDisplay == 1)
        {
            videoPlayer.PlayOutro();
        }
    }
}
