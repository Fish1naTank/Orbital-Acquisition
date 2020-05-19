using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class VelocityMeterReader : MonoBehaviour
{
    public enum VelocityVars { X, Y, Z };
    public VelocityVars velocityVar;

    public float multiplier = 1;

    public VelocityHud velocityHud;

    private RectTransform meter;
    public float zStartingRoatation;

    void Start()
    {
        meter = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        float height = 0;
        switch (velocityVar)
        {
            case VelocityVars.X:
                height = velocityHud.xVal;
                break;
            case VelocityVars.Y:
                height = velocityHud.yVal;
                break;
            case VelocityVars.Z:
                height = velocityHud.zVal;
                break;
        }

        height *= multiplier;

        if(height < 0)
        {
            meter.pivot = new Vector2(meter.pivot.x, 1);
            height *= -1;
        }
        else
        {
            meter.pivot = new Vector2(meter.pivot.x, 0);
        }

        meter.sizeDelta = new Vector2(meter.sizeDelta.x, height);
    }
}
