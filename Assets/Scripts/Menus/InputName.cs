using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    public Text inputText;

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
