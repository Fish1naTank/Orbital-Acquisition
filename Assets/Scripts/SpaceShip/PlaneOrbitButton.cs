using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneOrbitButton : MonoBehaviour
{
    public PlaneOrbitController planeOrbitController;
    public enum MovementAxis { X, Y, Roll }
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

    void FixedUpdate()
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
                planeOrbitController.XMoveDirection(direction);
                break;
            case MovementAxis.Y:
                planeOrbitController.YMoveDirection(direction);
                break;
            case MovementAxis.Roll:
                planeOrbitController.Roll(direction);
                break;
        }
    }
}
