using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed = 2;
    public bool orbitClockwise = true;

    private Vector3 offset;
    private Vector3 axis;

    void Start()
    {
        if(target == null)
        {
            target = this.transform;
        }

        offset = this.transform.position - target.transform.position;

        Vector3 worldVector;
        if (Mathf.Abs(offset.x) > offset.magnitude / 2)
        {
            worldVector = -Vector3.forward;
            if (orbitClockwise) worldVector = -worldVector;

            if (offset.x <= 0) worldVector = -worldVector;
        }
        else
        {
            worldVector = -Vector3.right;
            if (orbitClockwise) worldVector = -worldVector;

            if (offset.z >= 0) worldVector = -worldVector;
        }

        axis = Vector3.Cross(worldVector, offset);
    }

    void FixedUpdate()
    {
        //update postiton
        this.transform.position = target.transform.position + offset;

        //save rotation
        Quaternion currentRotation = this.transform.rotation;
            
        //normalize axis
        if (axis.magnitude != 1) axis.Normalize();
        //update orbit
        this.transform.RotateAround(target.transform.position, axis, orbitSpeed * Time.deltaTime);

        //reset correct rotation
        this.transform.rotation = currentRotation;

        //save offset
        offset = this.transform.position - target.transform.position;
    }
}
