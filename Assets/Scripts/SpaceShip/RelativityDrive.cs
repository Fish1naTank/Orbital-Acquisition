using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RelativityDrive : MonoBehaviour
{
    public float gravitationalConstant = 1;

    public GameObject lineObject;
    public int lineLength;
    public float spacing;
    public bool trackPath = false;

    public CelestialBody ClossestBody;
    private GameObject[] lineObjects;
    private Rigidbody rb;

    private Universe universe;

    void Start()
    {
        universe = FindObjectOfType<Universe>();

        rb = GetComponent<Rigidbody>();

        lineObjects = new GameObject[lineLength];
        for (int i = 0; i < lineObjects.Length; i++)
        {
            lineObjects[i] = (Instantiate(lineObject));
        }
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

        if (ClossestBody != null)
        {
            //calculate acceleration
            float sqrDist = (ClossestBody.transform.position - rb.position).sqrMagnitude;
            Vector3 forceDirection = (ClossestBody.transform.position - rb.position).normalized;
            Vector3 acceleration = forceDirection * Universe.gravitationalConstant * ClossestBody.mass / sqrDist;

            Vector3 LineVelocity = rb.velocity;

            rb.AddForce(acceleration);

            Vector3 newLinePosition = transform.position + LineVelocity;

            if(trackPath && lineObjects != null)
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
}
