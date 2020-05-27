using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SpaceObject), typeof(BoxCollider))]
public class SpaceDebris : MonoBehaviour
{
    Rigidbody rb;
    SpaceObject spaceObject;
    BoxCollider boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spaceObject = GetComponent<SpaceObject>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void PickUp()
    {
        spaceObject.enabled = false;
        boxCollider.enabled = false;
        rb.velocity = Vector3.zero;
    }
}
