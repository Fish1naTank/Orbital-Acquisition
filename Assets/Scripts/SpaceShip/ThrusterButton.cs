using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterButton : MonoBehaviour
{
    public TouchThruster TouchThruster;
    public enum MovementAxis { X, Y, Z, Pitch, Yaw, Roll, Orbit , Altitude }
    public MovementAxis movementAxis;
    public bool direction = true;

    private bool isPressed = false;

    public void OnPointerDown()
    {
        isPressed = true;
    }

    public void OnPointerUp()
    {
        isPressed = false;
    }

    void Update()
    {
        if(isPressed)
        {
            activateThruster();
        }
    }

    private void activateThruster()
    {
        switch (movementAxis)
        {
            case MovementAxis.X:
                TouchThruster.XAxisThruster(direction);
                break;
            case MovementAxis.Y:
                TouchThruster.YAxisThruster(direction);
                break;
            case MovementAxis.Z:
                TouchThruster.ZAxisThruster(direction);
                break;
            case MovementAxis.Roll:
                TouchThruster.RollThruster(direction);
                break;
            case MovementAxis.Orbit:
                TouchThruster.OrbitThruster(direction);
                break;
            case MovementAxis.Altitude:
                TouchThruster.AltitudeThruster(direction);
                break;
        }
    }
}
