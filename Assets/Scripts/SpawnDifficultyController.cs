using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDifficultyController : MonoBehaviour
{
    public GameObject easySpawner;
    public GameObject normalSpawner;

    void Start()
    {
        switch ((int)GameManager.instance.difficulty)
        {
            case 0:
                easySpawner.SetActive(true);
                break;
            case 1:
                normalSpawner.SetActive(true);
                break;
        }
    }
}
