using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rotate : MonoBehaviour
{
    public Vector3 axis;
    public float speed;
    public bool randomSpeed = false;

    public bool fowardFacing = false;

    Rigidbody rb;

    void Start()
    {
        if(fowardFacing)
        {
            rb = GetComponent<Rigidbody>();
        }

        if(randomSpeed)
        {
            speed = Random.value * speed * CoinFlip();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb != null)
        {
            FaceForward();
        }

        transform.Rotate(axis.normalized, speed * Time.deltaTime, Space.Self);
    }

    private void FaceForward()
    {
        transform.LookAt(transform.transform.position + rb.velocity.normalized, transform.transform.up);
    }

    private int CoinFlip()
    {
        if(Random.value >= 0.5f)
        {
            return 1;
        }

        return -1;
    }
}
