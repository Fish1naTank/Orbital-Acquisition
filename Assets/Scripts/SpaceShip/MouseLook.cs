using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MouseLook : MonoBehaviour
{
    public Transform ship;
    public float lookStrength = 2f;

    public Joystick joystick;

    private Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (joystick != null)
        {
            Vector3 lookVector = joystick.outputVector;
            Vector3 lookDirection = new Vector3(-lookVector.y, lookVector.x, 0) * lookStrength;
            cam.transform.localRotation = Quaternion.Euler(lookDirection);
        }
        else
        {
            Vector3 mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
            mousePos -= new Vector3(0.5f, 0.5f, 0);
            Vector3 lookDirection = new Vector3(-mousePos.y, mousePos.x, 0) * lookStrength;
            cam.transform.localRotation = Quaternion.Euler(lookDirection);
        }
    }
}
