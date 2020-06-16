using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinemaManager : MonoBehaviour
{
    public GameObject introPanelBoy;
    public VideoPlayer introPlayerBoy;
    public GameObject introPanelGirl;
    public VideoPlayer introPlayerGirl;
    public GameObject outroPanel;
    public VideoPlayer outroPlayer;

    void Start()
    {
        introPlayerBoy.loopPointReached += CheckIntroOver;
        introPlayerGirl.loopPointReached += CheckIntroOver;
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
        if(GameManager.instance.characterGirl)
        {
            introPanelGirl.SetActive(true);
        }
        else
        {
            introPanelBoy.SetActive(true);
        }
    }

    public void PlayOutro()
    {
        outroPanel.SetActive(true);
    }

    public void Play()
    {
        if(GameManager.instance.characterGirl)
        {
            introPlayerGirl.loopPointReached -= CheckIntroOver;
        }
        else
        {
            introPlayerBoy.loopPointReached -= CheckIntroOver;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
