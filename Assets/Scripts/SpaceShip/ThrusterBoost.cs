using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrusterBoost : MonoBehaviour
{
    public Image BoostImage;
    private RectTransform BoostImageTransform;

    public int activeThrusters = 0;

    public float maxBoost = 500;
    public float minBoost = 0;
    public float consumptionRate = 1;
    public float regenRate = 1;
    public float remainingBoost { get; private set; }

    void Start()
    {
        remainingBoost = maxBoost;
        BoostImageTransform = BoostImage.rectTransform;
        BoostImageTransform.sizeDelta = new Vector2(BoostImageTransform.sizeDelta.x, remainingBoost);

        setImageColor();
    }

    void FixedUpdate()
    {
        if(activeThrusters > 0)
        {
            UseBoost(activeThrusters);
        }
        else
        {
            RegenBoost();
        }
    }

    public void UpdateActiveThrusters(int thrusterCount)
    {
        activeThrusters += thrusterCount;
    }

    private void UseBoost(int activeThrusters)
    {
        remainingBoost -= activeThrusters * consumptionRate;
        if(remainingBoost < minBoost)
        {
            remainingBoost = minBoost;
        }

        UpdateBoostMeter();
    }

    private void RegenBoost()
    {
        remainingBoost += regenRate;
        if (remainingBoost > maxBoost)
        {
            remainingBoost = maxBoost;
        }

        UpdateBoostMeter();
    }

    private void UpdateBoostMeter()
    {
        BoostImageTransform.sizeDelta = new Vector2(BoostImageTransform.sizeDelta.x, remainingBoost);

        setImageColor();

        activeThrusters = 0;
    }

    private void setImageColor()
    {
        BoostImage.color = Color.Lerp(Color.red, Color.green, remainingBoost / maxBoost);
    }
}
