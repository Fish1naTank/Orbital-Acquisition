using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTimer : MonoBehaviour
{
    public float timer = 5f;

    private float elapsedTime;

    void OnEnable()
    {
        elapsedTime = 0;
    }

    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        if(elapsedTime > timer)
        {
            gameObject.SetActive(false);
        }
    }
}
