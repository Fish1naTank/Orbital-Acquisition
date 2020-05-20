using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpaceDebris")
        {
            Destroy(other.gameObject);
        }
    }
}
