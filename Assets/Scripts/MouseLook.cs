using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Vector2 lookSpeed = new Vector2(2.0f, 2.0f);

    public float yaw = 0.0f;
    public float pitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        yaw += lookSpeed.x * Input.GetAxis("Mouse X");
        pitch -= lookSpeed.y * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
