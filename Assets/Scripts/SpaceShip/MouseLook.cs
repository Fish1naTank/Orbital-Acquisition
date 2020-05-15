using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Vector2 lookSpeed = new Vector2(2.0f, 2.0f);

    public float yaw = 0.0f;
    public float pitch = 0.0f;

    public bool Active = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!Active) return;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active) return;

        yaw += lookSpeed.x * Input.GetAxis("Mouse X");
        pitch -= lookSpeed.y * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    public void ToggleActive()
    {
        if(Active)
        {
            Cursor.lockState = CursorLockMode.None;
            Active = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Active = true;
        }
    }
}
