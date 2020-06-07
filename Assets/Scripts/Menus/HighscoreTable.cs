using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HighscoreManager))]
public class HighscoreTable : MonoBehaviour
{
    public RectTransform entryContainer;
    public RectTransform entryTemplate;
    public RectTransform playerScoreContainer;
    public RectTransform playerScoreTemplate;
    public float templateHeight = 80;

    private HighscoreManager highscoreManager;

    private List<HighscoreEntry> highscoresEntryList;
    private List<Transform> highscoresTransformList;

    void Awake()
    {
        highscoreManager = GetComponent<HighscoreManager>();

        entryTemplate.gameObject.SetActive(false);
        playerScoreTemplate.gameObject.SetActive(false);

        DisplayPlayerScore();

        UpdateTable();
    }

    public void DisplayPlayerScore()
    {
        HighscoreEntry playerScore = new HighscoreEntry();
        playerScore.name = GameManager.instance.playerName;
        playerScore.score = GameManager.instance.gameScore;
        CreateHighscoreEntry(playerScore, playerScoreContainer, playerScoreTemplate);
    }

    public void UpdateTable()
    {
        if (highscoresTransformList != null)
        {
            foreach (Transform item in highscoresTransformList)
            {
                Destroy(item.gameObject);
            }
        }

        highscoresEntryList = new List<HighscoreEntry>();
        highscoresTransformList = new List<Transform>();

        if (highscoreManager != null)
        {
            var highscores = highscoreManager.LoadHighscores();
            if (highscores != null)
            {
                highscoresEntryList = highscores.highscoresList;
            }

            foreach (HighscoreEntry highscore in highscoresEntryList)
            {
                CreateHighscoreEntry(highscore, entryContainer, entryTemplate, highscoresTransformList);
            }
        }
    }

    private void CreateHighscoreEntry(HighscoreEntry highscoreEntry, RectTransform entryContainer, RectTransform entryTemplate, List<Transform> highscores = null)
    {
        int placement = 0;
        if (highscores != null)
        {
            placement = highscores.Count;
        }

        RectTransform entry = Instantiate(entryTemplate, entryContainer);
        entry.anchoredPosition = new Vector3(0, -templateHeight * placement);
        entry.gameObject.SetActive(true);

        entry.Find("Score").GetComponent<Text>().text = highscoreEntry.score.ToString();
        entry.Find("Name").GetComponent<Text>().text = highscoreEntry.name;

        if (highscores != null)
        {
            highscores.Add(entry);
        }
    }
}
