using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLock : MonoBehaviour
{
    public Rigidbody shipRigidbody;
    public SpaceObject TargetObject;

    //speed, altitude, angle
    public Vector3 TargetLocks;

    public float lockRange = 1;

    public bool enableAltitudeLock = false;
    public bool altitudeLocked = false;

    void FixedUpdate()
    {
        getTargetLocks();

        restrictAltitude();
    }


    private void getTargetLocks()
    {
        //speed
        TargetLocks.x = TargetObject.rb.velocity.magnitude;

        //altitude
        TargetLocks.y = (TargetObject.transform.position - TargetObject.ClossestBody.transform.position).magnitude;

        //angle
        TargetLocks.z = Vector3.Angle(Vector3.up, TargetObject.transform.position - TargetObject.ClossestBody.transform.position);
    }

    private void restrictAltitude()
    {
        if (!enableAltitudeLock) return;

        Vector3 celestialToShip = shipRigidbody.transform.position - TargetObject.ClossestBody.transform.position;
        if (altitudeLocked || celestialToShip.magnitude + lockRange > TargetLocks.y && celestialToShip.magnitude - lockRange < TargetLocks.y)
        {
            Vector3 lockPos = celestialToShip.normalized * TargetLocks.y;
            shipRigidbody.transform.position = lockPos;

            //kill velocity
            Vector3 zeroVelDirection = celestialToShip.normalized;
            Vector3 velocity = shipRigidbody.velocity;
            velocity -= Vector3.Dot(velocity, zeroVelDirection) * zeroVelDirection;
            shipRigidbody.velocity = velocity;

            altitudeLocked = true;
        }
    }

    public void ToggleAltitudeLock()
    {
        enableAltitudeLock = !enableAltitudeLock;
        altitudeLocked = false;

    }
}
