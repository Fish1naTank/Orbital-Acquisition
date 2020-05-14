using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceObject : MonoBehaviour
{
    public bool orbitClockwise = false;

    public CelestialBody _clossestBody;
    private FixedOrbit _clossestBodyOrbit;
    private Rigidbody rb;

    private Universe universe;

    // Start is called before the first frame update
    void Start()
    {
        universe = FindObjectOfType<Universe>();

        rb = GetComponent<Rigidbody>();

        findClossestBody();

        calculateOrbitalVelocity();
    }

    void FixedUpdate()
    {
        findClossestBody();
        addForce();
    }


    private void findClossestBody()
    {
        foreach (CelestialBody body in universe.CelestialBodies)
        {

            if (_clossestBody == null ||
                ((transform.position - body.transform.position).sqrMagnitude < (transform.position - _clossestBody.transform.position).sqrMagnitude))
            {
                if(_clossestBody != body)
                {
                    _clossestBody = body;
                    _clossestBodyOrbit = _clossestBody.GetComponent<FixedOrbit>();
                }
            }
        }

        checkCelestialOrbit();
    }

    private void checkCelestialOrbit()
    {
        if (_clossestBodyOrbit.target == null)
        {
            transform.parent = null;
            return;
        }

        transform.parent = _clossestBody.transform;
    }

    private void calculateOrbitalVelocity()
    {
        Vector3 meToCelestial = _clossestBody.transform.position - transform.position;

        //save transform
        Quaternion rotation = transform.rotation;
        Vector3 position = transform.position;

        transform.forward = meToCelestial.normalized;

        //get orbit vector
        Vector3 forceVector = transform.right;
        if (orbitClockwise) forceVector = -forceVector;

        //reset transform
        transform.position = position;
        transform.rotation = rotation;

        Vector3 orbitalForce = forceVector.normalized * Mathf.Sqrt(Universe.gravitationalConstant * _clossestBody.mass / meToCelestial.magnitude);
        rb.AddForce(orbitalForce, ForceMode.VelocityChange);
    }

    private void addForce()
    {
        Vector3 meToCelestial = _clossestBody.transform.position - transform.position;
        float sqrDist = (meToCelestial).sqrMagnitude;
        Vector3 forceDirection = meToCelestial.normalized;
        Vector3 acceleration = forceDirection * Universe.gravitationalConstant * _clossestBody.mass / sqrDist;

        rb.AddForce(acceleration, ForceMode.Acceleration);
    }
}
