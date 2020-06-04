using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retical : MonoBehaviour
{
    public Joystick joystick;
    public RectTransform ring;
    public RectTransform retical;

    void FixedUpdate()
    {
        retical.anchoredPosition = new Vector2(joystick.outputVector.x * ring.sizeDelta.x,
                                                joystick.outputVector.y * ring.sizeDelta.y) 
                                                * 0.5f;
    }
}
