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
    public RelativityDrive relativityDrive;
    public DifficultyController difficultyController;

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
        
        //adjust rotation
        Vector3 newRotation = Vector3.zero;
        newRotation.x = (Mathf.Abs(shipRigidbody.angularVelocity.x) > maxAngularVelocity) ? maxAngularVelocity * (shipRigidbody.angularVelocity.x > 0 ? 1 : -1) : shipRigidbody.angularVelocity.x;
        newRotation.y = (Mathf.Abs(shipRigidbody.angularVelocity.y) > maxAngularVelocity) ? maxAngularVelocity * (shipRigidbody.angularVelocity.y > 0 ? 1 : -1) : shipRigidbody.angularVelocity.y;
        newRotation.z = (Mathf.Abs(shipRigidbody.angularVelocity.z) > maxAngularVelocity) ? maxAngularVelocity * (shipRigidbody.angularVelocity.z > 0 ? 1 : -1) : shipRigidbody.angularVelocity.z;

        shipRigidbody.angularVelocity = newRotation;

    }

    private void Inputs()
    {
        switch (difficultyController.difficulty)
        {
            case DifficultyController.Difficulty.Normal:
                normalInputs();
                break;
            case DifficultyController.Difficulty.Hard:
                hardInputs();
                break;
            case DifficultyController.Difficulty.Simulation:
                simulationInputs();
                break;
        }

        if (_shipFuel.FuelRemaining > 0)
        {
            _shipFuel.UpdateActiveThrusters(_activeThrusterCount);
            _activeThrusterCount = 0;
        }
    }

    private void normalInputs()
    {
        if (Input.GetKey(KeyCode.E))
        {
            RollThruster(true);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            RollThruster(false);
        }

        if (Input.GetKey(KeyCode.X))
        {
            AltitudeThruster(true);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            AltitudeThruster(false);
        }
    }

    private void hardInputs()
    {
        if (Input.GetKey(KeyCode.E))
        {
            RollThruster(true);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            RollThruster(false);
        }

        if (Input.GetKey(KeyCode.X))
        {
            OrbitThruster(true);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            OrbitThruster(false);
        }
    }

    private void simulationInputs()
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

        if (Input.GetKey(KeyCode.X))
        {
            OrbitThruster(true);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            OrbitThruster(false);
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

    public void OrbitThruster(bool direction)
    {
        float multiplier = getMovementMultiplier();

        Vector3 thrustDirection = shipRigidbody.velocity.normalized;
        Vector3 movementForce = thrustDirection * (direction ? 1 : -1) * multiplier;

        addMoveThrusterForce(movementForce);
    }

    public void AltitudeThruster(bool direction)
    {
        Vector3 celestialToMe = shipRigidbody.transform.position - relativityDrive.ClossestBody.transform.position;

        shipRigidbody.transform.position += celestialToMe.normalized * (direction ? 1 : -1) * Time.deltaTime * getMovementMultiplier();

        //use thuster
        addMoveThrusterForce(Vector3.zero);
        //set orbital velocity
        shipRigidbody.velocity = calculateOrbitalVelocity();
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

    private Vector3 calculateOrbitalVelocity()
    {
        bool orbitClockwise = false;

        Vector3 meToCelestial = relativityDrive.ClossestBody.transform.position - shipRigidbody.transform.position;

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

        Vector3 orbitalForce = forceVector.normalized * Mathf.Sqrt(Universe.gravitationalConstant * relativityDrive.ClossestBody.mass / meToCelestial.magnitude);

        return orbitalForce;
    }
}
