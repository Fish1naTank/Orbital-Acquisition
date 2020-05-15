using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    public Image FuelImage;
    private RectTransform FuelImageTransform;

    public int activeThrusters = 0;

    public float fuelMax = 500;
    public float fuelmin = 0;
    public float fuleConsumptionRate = 1;
    public float FuelRemaining { get; private set; }

    void Start()
    {
        FuelRemaining = fuelMax;
        FuelImageTransform = FuelImage.rectTransform;
        FuelImageTransform.sizeDelta = new Vector2(FuelImageTransform.sizeDelta.x, FuelRemaining);

        setFuelImageColor();
    }

    void Update()
    {
        if(activeThrusters > 0)
        {
            burnFuel(activeThrusters);

            FuelImageTransform.sizeDelta = new Vector2 (FuelImageTransform.sizeDelta.x, FuelRemaining);

            setFuelImageColor();
        }
    }

    public void UpdateActiveThrusters(int thrusterCount)
    {
        activeThrusters = thrusterCount;
    }

    private void burnFuel(int activeThrusters)
    {
        FuelRemaining -= activeThrusters * fuleConsumptionRate;
        if(FuelRemaining < fuelmin)
        {
            FuelRemaining = fuelmin;
        }
    }

    private void setFuelImageColor()
    {
        FuelImage.color = Color.Lerp(Color.red, Color.green, FuelRemaining / fuelMax);
    }
}
