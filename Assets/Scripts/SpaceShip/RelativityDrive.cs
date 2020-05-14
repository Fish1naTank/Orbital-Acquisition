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
    private bool trackPath = false;

    public CelestialBody ClossestBody;
    private List<GameObject> lineObjects;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        lineObjects = new List<GameObject>();

        for (int i = 0; i < lineLength; i++)
        {
            lineObjects.Add(Instantiate(lineObject));
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

            Vector3 newLinePosition = this.transform.position + LineVelocity;

            if(trackPath)
            {
                //recalculate acceleration for each line object
                for (int i = 0; i < lineLength; i++)
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
        foreach (CelestialBody body in Universe.CelestialBodies)
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
