using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 axis;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(axis.normalized, speed * Time.deltaTime);
    }
}
