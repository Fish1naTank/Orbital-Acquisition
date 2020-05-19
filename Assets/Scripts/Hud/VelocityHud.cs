using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityHud : MonoBehaviour
{
    public Rigidbody ShipRigidbody;

    public Text xText;
    public Text yText;
    public Text zText;

    public float xVal;
    public float yVal;
    public float zVal;

    // Start is called before the first frame update
    void Start()
    {
        xText.text = "X: 0.00";
        yText.text = "Y: 0.00";
        zText.text = "Z: 0.00";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xVal = Vector3.Dot(ShipRigidbody.velocity, ShipRigidbody.transform.right);
        xText.text = "X: " + xVal.ToString("F2");

        yVal = Vector3.Dot(ShipRigidbody.velocity, ShipRigidbody.transform.up);
        yText.text = "Y: " + yVal.ToString("F2");

        zVal = Vector3.Dot(ShipRigidbody.velocity, ShipRigidbody.transform.forward);
        zText.text = "Z: " + zVal.ToString("F2");

    }
}
