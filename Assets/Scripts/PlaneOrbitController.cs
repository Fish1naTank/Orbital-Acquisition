using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThrusterBoost))]
public class PlaneOrbitController : MonoBehaviour
{
    public Transform ship;
    public Rigidbody orbitShipRigidbody;

    public float moveSpeed = 5;
    public float boostMultiplier = 2;
    public float rollSpeed = 2;

    public float maxOrbitShipDistance = 50;

    public Joystick joystick;

    private ThrusterBoost thusterBoost;
    private bool boost = false;

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
            ship.LookAt(ship.position + orbitShipRigidbody.velocity.normalized, ship.up);
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ActiveBoost(true);
        }

        /**
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
        /**/

        if(joystick != null)
        {
            VectorMoveDirection(joystick.outputVector);
        }

        Vector3 centerToMe = ship.position - orbitShipRigidbody.transform.position;
        float dist = centerToMe.magnitude;
        if (dist > maxOrbitShipDistance)
        {
            ship.position = orbitShipRigidbody.transform.position + centerToMe.normalized * maxOrbitShipDistance;
        }
    }

    public void ActiveBoost(bool active)
    {
        boost = active;
    }

    public void XMoveDirection(bool direction)
    {
        Vector3 moveDirection = ship.right * Time.deltaTime * (direction ? 1 : -1) * GetMoveSpeed();
        ship.position += moveDirection;
    }

    public void YMoveDirection(bool direction)
    {
        Vector3 moveDirection = ship.up * Time.deltaTime * (direction ? 1 : -1) * GetMoveSpeed();
        ship.position += moveDirection;
    }

    public void VectorMoveDirection(Vector2 direction)
    {
        Vector3 moveDirection = (ship.right * direction.x + ship.up * direction.y) * Time.deltaTime * GetMoveSpeed();
        ship.position += moveDirection;
    }

    public void Roll(bool direction)
    {
        float angle = Time.deltaTime * (direction ? 1 : -1) * rollSpeed;

        ship.Rotate(-Vector3.forward, angle);
    }

    private float GetMoveSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ActiveBoost(true);
        }

        if (boost)
        {
            ActiveBoost(false);

            thusterBoost.UpdateActiveThrusters(1);

            if (thusterBoost.BoostAvailable())
            {
                return moveSpeed * boostMultiplier;
            }
            else
            {
                return moveSpeed;
            }
        }

        return moveSpeed;
    }
}
