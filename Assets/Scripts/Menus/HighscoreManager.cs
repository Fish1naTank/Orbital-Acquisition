using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public int maxEntries = 5;

    public void ResetHighscores()
    {
        PlayerPrefs.DeleteKey("Highscores");
    }

    public void AddRandomScore()
    {
        int score = Random.Range(0, 100000);
        string name = "RNG";
        AddScore(score, name);
    }

    public void AddScore(int score, string name)
    {
        HighscoreEntry entry = new HighscoreEntry { score = score, name = name };

        Highscores highscores = LoadHighscores();

        if (highscores == null)
        {
            highscores = new Highscores();
            highscores.highscoresList = new List<HighscoreEntry>();
        }

        highscores.highscoresList.Add(entry);

        SortScores(highscores.highscoresList);

        SaveHighscores(highscores);
    }

    public Highscores LoadHighscores()
    {
        string json = PlayerPrefs.GetString("Highscores");
        return JsonUtility.FromJson<Highscores>(json);
    }

    private void SaveHighscores(Highscores highscores)
    {
        string jason = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("Highscores", jason);
        PlayerPrefs.Save();
    }

    private void SortScores(List<HighscoreEntry> scores)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            for (int j = i + 1; j < scores.Count; j++)
            {
                if (scores[j].score > scores[i].score)
                {
                    HighscoreEntry tmp = scores[i];
                    scores[i] = scores[j];
                    scores[j] = tmp;
                }
            }
        }

        //limit
        if (scores.Count > maxEntries)
        {
            for (int i = scores.Count; i > maxEntries; i--)
            {
                scores.RemoveAt(maxEntries);
            }
        }
    }

    public class Highscores
    {
        public List<HighscoreEntry> highscoresList;
    }
}
