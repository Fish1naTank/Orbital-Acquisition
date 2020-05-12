using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipGravity : MonoBehaviour
{
    public GameObject[] CelestialBodies;
    public float force = 10;

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
            Vector3 forceDirection = clossestObject.transform.position - this.transform.position;

            float pullforce = clossestObject.transform.localScale.x / forceDirection.magnitude * force;

            Vector3 addForce = forceDirection.normalized * pullforce * Time.deltaTime;

            rb.AddForce(addForce);

            Vector3 LineVelocity = rb.velocity;
            Vector3 newLinePosition = this.transform.position + LineVelocity;

            if(trackPath)
            {
                for (int i = 0; i < lineLength; i++)
                {
                    lineObjects[i].transform.position = newLinePosition;

                    forceDirection = clossestObject.transform.position - lineObjects[i].transform.position;

                    pullforce = clossestObject.transform.localScale.x / forceDirection.magnitude * force;

                    addForce = forceDirection.normalized * pullforce * Time.deltaTime;

                    LineVelocity += addForce / spacing;
                    newLinePosition += LineVelocity;
                }
            }
        }
    }

    private void findClossestBody()
    {
        foreach (GameObject body in CelestialBodies)
        {
            if (clossestObject == null)
            {
                clossestObject = body;
                return;
            }

            if ((this.transform.position - body.transform.position).sqrMagnitude < (this.transform.position - clossestObject.transform.position).sqrMagnitude)
            {
                clossestObject = body;
            }
        }
    }
}
