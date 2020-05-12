using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceObjectSpawner : MonoBehaviour
{
    public GameObject[] SpaceObjectsToSpawn;
    public int numberOfObjectsToSpawn = 100;
    public Vector2 distFromCenter = new Vector2(500, 800);

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
            spaceObject.transform.localScale = new Vector3(1, 1, 1) * Random.Range(3, 10);
            //set object orbit
            Orbit spaceObjectOrbit = spaceObject.GetComponent<Orbit>();
            spaceObjectOrbit.target = this.transform;
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;

        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + Random.Range(0, distFromCenter.y - distFromCenter.x);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

        return pos;
    }
}
