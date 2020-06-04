using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class ColorToggle : MonoBehaviour
{
    public Color offColor;
    public Color onColor;

    public bool startState = false;
    public bool permanent = false;

    private Image image;
    private bool on = false;

    void Start()
    {
        image = GetComponent<Image>();

        on = startState;

        updateColor();
    }

    public void OnPointerUp()
    {
        if (permanent)
        {
            if (!on)
            {
                on = true;
            }
        }
        else
        {
            on = !on;
        }

        updateColor();
    }

    private void updateColor()
    {
        if (on)
        {
            image.color = onColor;
        }
        else
        {
            image.color = offColor;
        }
    }
}
