using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    public static float gravitationalConstant = 0.75f;
    public CelestialBody[] CelestialBodies;


    void OnValidate()
    {
        CelestialBodies = FindObjectsOfType<CelestialBody>();
    }
}
