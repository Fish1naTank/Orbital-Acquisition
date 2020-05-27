using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedOrbit))]
public class CelestialBody : MonoBehaviour
{
    public float mass;

    void OnValidate()
    {
        mass = transform.localScale.x * transform.localScale.x;
    }
}
