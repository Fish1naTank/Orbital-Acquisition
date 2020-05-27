using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThrusterBoost))]
public class PlaneOrbitController : MonoBehaviour
{
    public Camera cam;
    public Rigidbody orbitShipRigidbody;

    public float moveSpeed = 5;
    public float boostMultiplier = 2;
    public float rollSpeed = 2;

    public float maxOrbitShipDistance = 50;

    private ThrusterBoost thusterBoost;

    void Start()
    {
        thusterBoost = GetComponent<ThrusterBoost>();
    }

    void FixedUpdate()
    {
        Rotation();
        Movement();
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Roll(true);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Roll(false);
        }

        if (orbitShipRigidbody.velocity.sqrMagnitude > 5)
        {
            cam.transform.LookAt(cam.transform.position + orbitShipRigidbody.velocity.normalized, cam.transform.up);
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            XMoveDirection(true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            XMoveDirection(false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            YMoveDirection(true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            YMoveDirection(false);
        }

        Vector3 centerToMe = cam.transform.position - orbitShipRigidbody.transform.position;
        float dist = centerToMe.magnitude;
        if (dist > maxOrbitShipDistance)
        {
            cam.transform.position = orbitShipRigidbody.transform.position + centerToMe.normalized * maxOrbitShipDistance;
        }
    }

    public void XMoveDirection(bool direction)
    {
        Vector3 moveDirection = cam.transform.right * Time.deltaTime * (direction ? 1 : -1) * GetMoveSpeed();
        cam.transform.position += moveDirection;
    }

    public void YMoveDirection(bool direction)
    {
        Vector3 moveDirection = cam.transform.up * Time.deltaTime * (direction ? 1 : -1) * GetMoveSpeed();
        cam.transform.position += moveDirection;
    }

    public void Roll(bool direction)
    {
        float angle = Time.deltaTime * (direction ? 1 : -1) * rollSpeed;

        cam.transform.Rotate(-Vector3.forward, angle);
    }

    private float GetMoveSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            thusterBoost.UpdateActiveThrusters(1);
            return moveSpeed * boostMultiplier;
        }
        return moveSpeed;
    }
}
