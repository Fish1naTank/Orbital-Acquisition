using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThruster : MonoBehaviour
{
    public GameObject playerCamera;
    public Rigidbody playerShip;

    public float thrusterForce = 20;

    private float thustTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = playerCamera.transform.rotation;

        thrust();
    }

    private void thrust()
    {
        Vector3 thrustDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            thrustDirection += this.transform.forward;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            thrustDirection -= this.transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            thrustDirection += this.transform.right;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            thrustDirection -= this.transform.right;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            thrustDirection += this.transform.up;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            thrustDirection -= this.transform.up;
        }


        if (thrustDirection != Vector3.zero)
        {
            thustTime += Time.deltaTime;

            playerShip.AddForce(thrustDirection.normalized * thustTime * thrusterForce);
        }
        else
        {
            thustTime = 0;
        }
    }
}
