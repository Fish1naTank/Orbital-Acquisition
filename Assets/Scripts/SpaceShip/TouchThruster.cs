using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Fuel))]
public class TouchThruster : MonoBehaviour
{
    public Camera shipCam;
    public Rigidbody shipRigidbody;
    public MouseLook mouseLook;

    //Movement Variables
    public bool preciseMovement = true;
    public float movementMultiplier = 2;
    public float preciseMovementMultiplier = 1;
    //x, y ,z
    public Vector3 movementRate { get; private set; }

    //Rotation Variables
    public bool preciseRotation = true;
    public float rotationMultiplier = 2;
    public float preciseRotationMultiplier = 1;

    public float maxAngularVelocity = 0.5f;

    private Fuel _shipFuel;
    private int _activeThrusterCount = 0;

    void Start()
    {
        _shipFuel = GetComponent<Fuel>();
    }
    
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Inputs();

        if (!mouseLook.Active)
        {
            //adjust rotation
            Vector3 newRotation = Vector3.zero;
            newRotation.x = (Mathf.Abs(shipRigidbody.angularVelocity.x) > maxAngularVelocity) ? maxAngularVelocity * (shipRigidbody.angularVelocity.x > 0 ? 1 : -1) : shipRigidbody.angularVelocity.x;
            newRotation.y = (Mathf.Abs(shipRigidbody.angularVelocity.y) > maxAngularVelocity) ? maxAngularVelocity * (shipRigidbody.angularVelocity.y > 0 ? 1 : -1) : shipRigidbody.angularVelocity.y;
            newRotation.z = (Mathf.Abs(shipRigidbody.angularVelocity.z) > maxAngularVelocity) ? maxAngularVelocity * (shipRigidbody.angularVelocity.z > 0 ? 1 : -1) : shipRigidbody.angularVelocity.z;

            shipRigidbody.angularVelocity = newRotation;
        }

    }

    private void Inputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            YAxisThruster(true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            YAxisThruster(false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            XAxisThruster(true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            XAxisThruster(false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            ZAxisThruster(true);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            ZAxisThruster(false);
        }

        if (Input.GetKey(KeyCode.E))
        {
            RollThruster(true);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            RollThruster(false);
        }

        if (mouseLook.Active)
        {
            this.transform.rotation = shipCam.transform.rotation;
        }

        if (_shipFuel.FuelRemaining > 0)
        {
            _shipFuel.UpdateActiveThrusters(_activeThrusterCount);
            _activeThrusterCount = 0;
        }
    }

    public void TogglePreciseMovement()
    {
        preciseMovement = !preciseMovement;
    }

    public void XAxisThruster(bool direction)
    {
        float multiplier = getMovementMultiplier();

        Vector3 movementForce = transform.right * (direction ? 1 : -1) * multiplier;

        addMoveThrusterForce(movementForce);
    }

    public void YAxisThruster(bool direction)
    {
        float multiplier = getMovementMultiplier();

        Vector3 movementForce = transform.up * (direction ? 1 : -1) * multiplier;

        addMoveThrusterForce(movementForce);
    }

    public void ZAxisThruster(bool direction)
    {
        float multiplier = getMovementMultiplier();

        Vector3 movementForce = transform.forward * (direction ? 1 : -1) * multiplier;

        addMoveThrusterForce(movementForce);
    }

    public void TogglePreciseRotation()
    {
        preciseRotation = !preciseRotation;
    }

    public void LookThruster(Vector2 rotationVector)
    {
        float multiplier = getRotationMultiplier();

        Vector2 rotationForce = rotationVector.normalized * multiplier;

        shipRigidbody.AddRelativeTorque(rotationForce);
    }

    public void RollThruster(bool direction)
    {
        float multiplier = getRotationMultiplier();

        Vector3 rotationForce = -Vector3.forward * (direction ? 1 : -1) * multiplier * 0.5f;

        shipRigidbody.AddRelativeTorque(rotationForce);
    }

    private float getMovementMultiplier()
    {
        return preciseMovement? preciseMovementMultiplier : movementMultiplier;
    }
    
    private float getRotationMultiplier()
    {
        return preciseRotation? preciseRotationMultiplier : rotationMultiplier;
    }

    private void addMoveThrusterForce(Vector3 movementForce)
    {
        _activeThrusterCount += 1;
        shipRigidbody.AddForce(movementForce);
    }
}
