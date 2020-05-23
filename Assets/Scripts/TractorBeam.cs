using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public float shrinkSpeed = 0.1f;

    private List<GameObject> itemsToCollect;

    void Start()
    {
        itemsToCollect = new List<GameObject>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpaceDebris")
        {
            itemsToCollect.Add(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject debris in itemsToCollect.ToList())
        {
            Vector3 shrinkVector = Vector3.one * shrinkSpeed;
            debris.transform.localScale -= shrinkVector;

            if(debris.transform.localScale.x < 0.1f || debris.transform.localScale.y < 0.1f || debris.transform.localScale.z < 0.1f)
            {
                itemsToCollect.Remove(debris);
                Destroy(debris);
            }
        }
    }
}
