using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public enum Difficulty { Normal, Hard, Simulation }
    public Difficulty difficulty = Difficulty.Normal;

    public GameObject[] NormalControlls;
    public GameObject[] HardControlls;
    public GameObject[] SimulationControlls;

    // Start is called before the first frame update
    void Start()
    {
        setDifficulty();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void setDifficulty()
    {
        enableControls(Difficulty.Normal, false);
        enableControls(Difficulty.Hard, false);
        enableControls(Difficulty.Simulation, false);

        switch (difficulty)
        {
            case Difficulty.Normal:
                enableControls(Difficulty.Normal, true);
                break;

            case Difficulty.Hard:
                enableControls(Difficulty.Hard, true);
                break;

            case Difficulty.Simulation:
                enableControls(Difficulty.Simulation, true);
                break;
        }
    }

    private void enableControls(Difficulty difficulty, bool enable)
    {
        switch (difficulty)
        {
            case Difficulty.Normal:
                for (int i = 0; i < NormalControlls.Length; i++)
                {
                    NormalControlls[i].SetActive(enable);
                }
                break;

            case Difficulty.Hard:
                for (int i = 0; i < HardControlls.Length; i++)
                {
                    HardControlls[i].SetActive(enable);
                }
                break;

            case Difficulty.Simulation:
                for (int i = 0; i < SimulationControlls.Length; i++)
                {
                    SimulationControlls[i].SetActive(enable);
                }
                break;
        }
    }
}
