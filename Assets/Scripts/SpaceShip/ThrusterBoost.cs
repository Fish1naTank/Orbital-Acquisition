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

    public float boostAvailableThreshold = 20;

    public float maxBoost = 500f;
    public float minBoost = 0f;
    public float consumptionRate = 1f;
    public float regenRate = 1f;
    public float remainingBoost { get; private set; }

    void Start()
    {
        consumptionRate = (int)GameManager.instance.difficulty + 1;

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

    public bool BoostAvailable()
    {
        if(remainingBoost / maxBoost > boostAvailableThreshold)
        {
            return true;
        }

        return false;
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
