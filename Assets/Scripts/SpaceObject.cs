using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceObject : MonoBehaviour
{
    public bool orbitClockwise = false;

    public bool giveOrbitVelocity = true;

    public GameObject lineObject;
    public int lineLength;
    public float spacing;
    public bool trackPath = false;

    public CelestialBody ClossestBody;
    private FixedOrbit _clossestBodyOrbit;
    private GameObject[] lineObjects;
    private Rigidbody rb;

    private Universe universe;

    // Start is called before the first frame update
    void Start()
    {
        universe = FindObjectOfType<Universe>();

        rb = GetComponent<Rigidbody>();

        if (lineObject != null)
        {
            lineObjects = new GameObject[lineLength];
            for (int i = 0; i < lineObjects.Length; i++)
            {
                lineObjects[i] = (Instantiate(lineObject));
            }
        }

        findClossestBody();

        if(giveOrbitVelocity) calculateOrbitalVelocity();
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

            if (ClossestBody == null ||
                ((transform.position - body.transform.position).sqrMagnitude < (transform.position - ClossestBody.transform.position).sqrMagnitude))
            {
                if(ClossestBody != body)
                {
                    ClossestBody = body;
                    _clossestBodyOrbit = ClossestBody.GetComponent<FixedOrbit>();
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

        transform.parent = ClossestBody.transform;
    }

    private void calculateOrbitalVelocity()
    {
        Vector3 meToCelestial = ClossestBody.transform.position - transform.position;

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

        Vector3 orbitalForce = forceVector.normalized * Mathf.Sqrt(Universe.gravitationalConstant * ClossestBody.mass / meToCelestial.magnitude);
        rb.AddForce(orbitalForce, ForceMode.VelocityChange);
    }

    private void addForce()
    {
        Vector3 meToCelestial = ClossestBody.transform.position - transform.position;
        float sqrDist = (meToCelestial).sqrMagnitude;
        Vector3 forceDirection = meToCelestial.normalized;
        Vector3 acceleration = forceDirection * Universe.gravitationalConstant * ClossestBody.mass / sqrDist;

        Vector3 LineVelocity = rb.velocity;

        rb.AddForce(acceleration, ForceMode.Acceleration);

        Vector3 newLinePosition = transform.position + LineVelocity;

        if (trackPath && lineObjects != null)
        {
            //recalculate acceleration for each line object
            for (int i = 0; i < lineObjects.Length; i++)
            {
                lineObjects[i].transform.position = newLinePosition;

                sqrDist = (ClossestBody.transform.position - lineObjects[i].transform.position).sqrMagnitude;
                forceDirection = (ClossestBody.transform.position - lineObjects[i].transform.position).normalized;
                acceleration = forceDirection * Universe.gravitationalConstant * ClossestBody.mass / sqrDist;

                LineVelocity += acceleration * spacing;
                newLinePosition += LineVelocity;
            }
        }
    }
}
