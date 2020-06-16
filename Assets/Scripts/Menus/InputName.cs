using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputName : MonoBehaviour
{
    public TMP_Text inputText;

    public void SetName()
    {
        if (inputText.text == "")
        {
            GameManager.instance.SetName("SAM");
        }
        else
        {
            GameManager.instance.SetName(inputText.text);
        }
    }
}
