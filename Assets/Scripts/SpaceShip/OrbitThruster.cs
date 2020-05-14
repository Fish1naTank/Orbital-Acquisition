using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitThruster : MonoBehaviour
{
    public RelativityDrive RelativityDrive;
    public Camera PlayerCamera;
    public Rigidbody PlayerShip;

    public float thrusterForce = 20;

    //private Fuel _shipFuel;
    private int _activeThrusterCount;
    private float _thustTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        thrust();
    }

    private void thrust()
    {
        if (RelativityDrive.ClossestBody == null) return;
        Vector3 celestialPos = RelativityDrive.ClossestBody.transform.position;
        Vector3 ourPos = transform.position;

        Vector3 meToCelestial = celestialPos - ourPos;

        PlayerShip.transform.forward = meToCelestial.normalized;

        Vector3 thrustDirection = Vector3.zero;
        _activeThrusterCount = 0;

        if (Input.GetKey(KeyCode.W))
        {
            thrustDirection += this.transform.forward;
            _activeThrusterCount++;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            thrustDirection -= this.transform.forward;
            _activeThrusterCount++;
        }

        if (Input.GetKey(KeyCode.D))
        {
            thrustDirection += this.transform.right;
            _activeThrusterCount++;
        }
        else if (Input.GetKey(KeyCode.A))
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

            PlayerShip.AddForce(thrustDirection.normalized * _thustTime * thrusterForce);
        }
        else
        {
            _thustTime = 0;
        }
    }
}
