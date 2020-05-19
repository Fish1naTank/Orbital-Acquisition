using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class RelativityDrive : MonoBehaviour
{
    public float gravitationalConstant = 1;

    public bool trackPath = false;
    [ColorUsage(true, true)]
    public Color pathColor;
    public int lineNodeCount = 100;

    public CelestialBody ClossestBody;
    private Rigidbody rb;
    private LineRenderer lr;

    private Universe universe;

    void Start()
    {
        universe = FindObjectOfType<Universe>();

        rb = GetComponent<Rigidbody>();

        lr = GetComponent<LineRenderer>();
        lr.startColor = pathColor;
        lr.endColor = pathColor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            trackPath = !trackPath;
        }
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
            if (ClossestBody == null)
            {
                ClossestBody = body;
                transform.parent = body.transform;
                return;
            }

            if ((transform.position - body.transform.position).sqrMagnitude < (transform.position - ClossestBody.transform.position).sqrMagnitude)
            {
                if(ClossestBody != body)
                {
                    //make ourmovemnt relative to clossest object
                    transform.parent = body.transform;

                    ClossestBody = body;
                }
            }
        }
    }

    private void addForce()
    {
        if (ClossestBody != null)
        {
            //calculate acceleration
            float sqrDist = (ClossestBody.transform.position - rb.position).sqrMagnitude;
            Vector3 forceDirection = (ClossestBody.transform.position - rb.position).normalized;
            Vector3 acceleration = forceDirection * Universe.gravitationalConstant * ClossestBody.mass / sqrDist;

            rb.AddForce(acceleration, ForceMode.Acceleration);

            Vector3 NodeVelocity = rb.velocity;
            Vector3 newNodePosition = rb.position + NodeVelocity;

            if (trackPath)
            {
                if (lr.positionCount != lineNodeCount) lr.positionCount = lineNodeCount;

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
}
