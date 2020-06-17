using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSettings : MonoBehaviour
{
    public void SetCharacter(bool isGirl)
    {
        GameManager.instance.SetCharacter(isGirl);
    }

    public void SetDifficulty(int difficulty)
    {
        GameManager.instance.SetDifficulty(difficulty);
    }

    public void ReturnToMenu()
    {
        GameManager.instance.ReturnToMenu();
    }
}
