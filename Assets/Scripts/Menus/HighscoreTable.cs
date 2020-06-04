using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HighscoreManager))]
public class HighscoreTable : MonoBehaviour
{
    public RectTransform entryContainer;
    public RectTransform entryTemplate;
    public float templateHeight = 80;

    private HighscoreManager highscoreManager;

    private List<HighscoreEntry> highscoresEntryList;
    private List<Transform> highscoresTransformList;

    void Awake()
    {
        highscoreManager = GetComponent<HighscoreManager>();

        entryTemplate.gameObject.SetActive(false);

        UpdateTable();
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

        var highscores = highscoreManager.LoadHighscores();
        if (highscores != null)
        {
            highscoresEntryList = highscores.highscoresList;
        }

        foreach (HighscoreEntry highscore in highscoresEntryList)
        {
            CreateHighscoreEntry(highscore, entryContainer, highscoresTransformList);
        }
    }

    private void CreateHighscoreEntry(HighscoreEntry highscoreEntry, RectTransform entryContainer, List<Transform> highscores)
    {
        RectTransform entry = Instantiate(entryTemplate, entryContainer);
        entry.anchoredPosition = new Vector3(0, -templateHeight * highscores.Count);
        entry.gameObject.SetActive(true);

        entry.Find("Score").GetComponent<Text>().text = highscoreEntry.score.ToString();
        entry.Find("Name").GetComponent<Text>().text = highscoreEntry.name;

        highscores.Add(entry);
    }
}
