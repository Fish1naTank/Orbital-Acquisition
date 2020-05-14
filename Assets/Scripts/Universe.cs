using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    public static float gravitationalConstant = 500;
    public static CelestialBody[] CelestialBodies;

    void OnValidate()
    {
        CelestialBodies = FindObjectsOfType<CelestialBody>();
    }
}
