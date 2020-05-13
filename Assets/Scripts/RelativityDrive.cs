using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RelativityDrive : MonoBehaviour
{
    public Orbit[] CelestialBodies;
    public float gravitationalConstant = 1;

    public GameObject lineObject;
    public int lineLength;
    public float spacing;
    private bool trackPath = false;

    private GameObject clossestObject;
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

        if (clossestObject != null)
        {
            //calculate addForce
            Vector3 forceDirection = clossestObject.transform.position - this.transform.position;

            float pullforce = clossestObject.transform.localScale.x / forceDirection.magnitude * clossestObject.transform.localScale.x * gravitationalConstant;

            Vector3 addForce = forceDirection.normalized * pullforce * Time.deltaTime;

            Vector3 LineVelocity = rb.velocity;

            rb.AddForce(addForce);

            Vector3 newLinePosition = this.transform.position + LineVelocity;

            if(trackPath)
            {
                //recalculate addForce for each line object
                for (int i = 0; i < lineLength; i++)
                {
                    lineObjects[i].transform.position = newLinePosition;

                    forceDirection = clossestObject.transform.position - lineObjects[i].transform.position;

                    pullforce = clossestObject.transform.localScale.x / forceDirection.magnitude * clossestObject.transform.localScale.x * gravitationalConstant;

                    addForce = forceDirection.normalized * pullforce * Time.deltaTime;

                    LineVelocity += addForce / spacing;
                    newLinePosition += LineVelocity;
                }
            }
        }
    }

    private void findClossestBody()
    {
        foreach (Orbit body in CelestialBodies)
        {
            if (clossestObject == null)
            {
                clossestObject = body.gameObject;
                return;
            }

            if ((this.transform.position - body.transform.position).sqrMagnitude < (this.transform.position - clossestObject.transform.position).sqrMagnitude)
            {
                if(clossestObject != body)
                {
                    //make CelestialBodies orbit relative to clossest object
                    Orbit oldCenter = clossestObject.GetComponent<Orbit>();

                    oldCenter.target = body.transform;
                    oldCenter.orbitSpeed = body.orbitSpeed;
                    oldCenter.orbitClockwise = body.orbitClockwise;

                    body.target = null;

                    clossestObject = body.gameObject;
                }
            }
        }
    }
}
