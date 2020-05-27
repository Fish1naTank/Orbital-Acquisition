using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceObjectSpawner : MonoBehaviour
{
    public GameObject[] SpaceObjectsToSpawn;
    public int numberOfObjectsToSpawn = 100;
    public Vector3 distFromCenter = new Vector3(300, 400, 100);

    public bool varyingObjectSize = false;

    void Start()
    {
        Transform parentTransform = this.GetComponentInParent<Transform>();

        spawnObjects();
    }

    private void spawnObjects()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector3 pos = RandomCircle(center, Random.Range(distFromCenter.x, distFromCenter.y));
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            GameObject spaceObject;
            spaceObject = Instantiate(SpaceObjectsToSpawn[Random.Range(0, SpaceObjectsToSpawn.Length)], pos, rot);

            //set object size
            Vector3 size = Vector3.one;
            if(varyingObjectSize)
            {
                size *= Random.Range(3, 10);
            }
            spaceObject.transform.localScale = size;
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;

        float donutRange = distFromCenter.z * 0.5f;

        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + Random.Range(-donutRange, donutRange);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

        return pos;
    }
}
