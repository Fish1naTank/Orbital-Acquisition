using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class SpaceObject : MonoBehaviour
{
    public bool orbitClockwise = false;

    public bool giveOrbitVelocity = true;

    public bool trackPath = false;
    [ColorUsage(true, true)]
    public Color pathColor;
    public int lineNodeCount = 100;

    public Rigidbody rb { get; private set; }
    public CelestialBody ClossestBody;
    private LineRenderer lr;
    private FixedOrbit _clossestBodyOrbit;

    private Universe universe;

    // Start is called before the first frame update
    void Start()
    {
        universe = FindObjectOfType<Universe>();

        rb = GetComponent<Rigidbody>();

        lr = GetComponent<LineRenderer>();
        lr.positionCount = lineNodeCount;
        lr.startColor = pathColor;
        lr.endColor = pathColor;

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

        rb.AddForce(acceleration, ForceMode.Acceleration);

        Vector3 NodeVelocity = rb.velocity;
        Vector3 newNodePosition = transform.position + NodeVelocity;

        if (trackPath)
        {
            if(lr.positionCount != lineNodeCount) lr.positionCount = lineNodeCount;

            Vector3[] orbitPositions = new Vector3[lineNodeCount];

            //recalculate acceleration for each line node
            for (int i = 0; i < lineNodeCount; i++)
            {
                orbitPositions[i] = newNodePosition;

                sqrDist = (ClossestBody.transform.position - orbitPositions[i]).sqrMagnitude;
                forceDirection = (ClossestBody.transform.position - orbitPositions[i]).normalized;
                acceleration = forceDirection * Universe.gravitationalConstant * ClossestBody.mass / sqrDist;

                NodeVelocity += acceleration;
                newNodePosition += NodeVelocity;
            }

            lr.SetPositions(orbitPositions);
        }
    }
}
