using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinemaManager : MonoBehaviour
{
    public GameObject introPanel;
    public VideoPlayer introPlayer;
    public GameObject outroPanel;
    public VideoPlayer outroPlayer;

    void Start()
    {
        introPlayer.loopPointReached += CheckIntroOver;
        outroPlayer.loopPointReached += CheckOutroOver;
    }

    void CheckIntroOver(UnityEngine.Video.VideoPlayer vp)
    {
        vp.loopPointReached -= CheckIntroOver;
        Play();
    }

    void CheckOutroOver(UnityEngine.Video.VideoPlayer vp)
    {
        vp.loopPointReached -= CheckOutroOver;
        outroPanel.SetActive(false);
    }

    public void PlayIntro()
    {
        introPanel.SetActive(true);
    }

    public void PlayOutro()
    {
        outroPanel.SetActive(true);
    }

    public void Play()
    {
        introPlayer.loopPointReached -= CheckIntroOver;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
