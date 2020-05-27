using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OrbitShipController : MonoBehaviour
{
    public Camera cam;
    Rigidbody orbitShipRigidbody;

    void Start()
    {
        orbitShipRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
    }
}
