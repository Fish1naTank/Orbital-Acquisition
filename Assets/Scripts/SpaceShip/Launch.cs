using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
[RequireComponent(typeof(Rigidbody))]
public class Launch : MonoBehaviour
{
    public bool launchCompleate { get; private set; }

    public GameObject launchButton;
    public RelativityDrive relativityDrive;
    public CelestialBody planet;
    public float targetAltitude;
    public bool orbitClockwise = false;

    public float thrustForce = 12;

    private Rigidbody rb;
    private Launch launch;
    private Vector3 launchVector;
    private float startingAltitude;

    private bool startLaunch = false;

    void Start()
    {
        launchCompleate = false;

        rb = GetComponent<Rigidbody>();

        Vector3 celestialToMe = transform.position - planet.transform.position;
        launchVector = celestialToMe.normalized;

        startingAltitude = celestialToMe.magnitude;
    }

    void FixedUpdate()
    {
        if (startLaunch)
        {
            executeLaunch();
        }
    }

    public void InitiateLaunch()
    {
        startLaunch = true;

        if (relativityDrive != null)
        {
            //relativityDrive.trackPath = true;
        }
    }

    private void executeLaunch()
    {
        Vector3 meToCelestial = planet.transform.position - transform.position;
        float currentAltitude = meToCelestial.magnitude;

        /**
        float sqrDist = (meToCelestial).sqrMagnitude;
        float acceleration = Universe.gravitationalConstant * planet.mass / sqrDist;

        Vector3 launchVelocity;
        /**/

        if (currentAltitude < targetAltitude)
        {
            /**/
            float lerpTime = (currentAltitude - startingAltitude) / (targetAltitude - startingAltitude);
            Vector3 targetVelocityVector = calculateOrbitalVelocity(targetAltitude);
            Vector3 thrustVector = Vector3.Lerp(launchVector, targetVelocityVector, lerpTime);

            thrustVector = thrustVector.normalized * thrustForce;
            if (currentAltitude - startingAltitude > (targetAltitude - startingAltitude) * 0.1f)
            {
                //rb.AddForce(targetVelocityVector.normalized);
            }
            else
            {
                rb.AddForce(thrustVector);
            }
            /**/
        }
        else
        {
            endLaunch();
        }
    }

    private void endLaunch()
    {
        rb.velocity = calculateOrbitalVelocity(targetAltitude);
        if (launchButton != null) launchButton.SetActive(false);
        launchCompleate = true;
        enabled = false;
    }

    private Vector3 calculateOrbitalVelocity(float altitude)
    {
        Vector3 meToCelestial = planet.transform.position - transform.position;
        meToCelestial = altitude * meToCelestial.normalized;

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

        Vector3 orbitalForce = forceVector.normalized * Mathf.Sqrt(Universe.gravitationalConstant * planet.mass / meToCelestial.magnitude);

        return orbitalForce;
    }
}
