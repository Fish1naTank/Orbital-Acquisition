using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fuel))]
public class ShipThruster : MonoBehaviour
{
    public Camera playerCamera;
    public Rigidbody playerShip;

    public float thrusterForce = 20;

    private Fuel _shipFuel;
    private int _activeThrusterCount;
    private float _thustTime;

    // Start is called before the first frame update
    void Start()
    {
        _shipFuel = GetComponent<Fuel>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = playerCamera.transform.rotation;

        if (_shipFuel.FuelRemaining > 0)
        {
            _shipFuel.UpdateActiveThrusters(GetActiveThrusterCount());
            thrust();
        }
    }

    private void thrust()
    {
        Vector3 thrustDirection = Vector3.zero;
        _activeThrusterCount = 0;

        if (Input.GetKey(KeyCode.W))
        {
            thrustDirection += this.transform.forward;
            _activeThrusterCount++;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            thrustDirection -= this.transform.forward;
            _activeThrusterCount++;
        }

        if (Input.GetKey(KeyCode.D))
        {
            thrustDirection += this.transform.right;
            _activeThrusterCount++;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            thrustDirection -= this.transform.right;
            _activeThrusterCount++;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            thrustDirection += this.transform.up;
            _activeThrusterCount++;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            thrustDirection -= this.transform.up;
            _activeThrusterCount++;
        }


        if (thrustDirection != Vector3.zero)
        {
            _thustTime += Time.deltaTime;

            playerShip.AddForce(thrustDirection.normalized * _thustTime * thrusterForce);
        }
        else
        {
            _thustTime = 0;
        }
    }

    public int GetActiveThrusterCount()
    {
        return _activeThrusterCount;
    }
}
