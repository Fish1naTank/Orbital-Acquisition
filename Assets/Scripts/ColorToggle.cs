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

    private Image image;
    private bool on = false;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerUp()
    {
        on = !on;

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
