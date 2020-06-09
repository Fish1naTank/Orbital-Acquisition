using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageController : MonoBehaviour
{
    public GameObject damageAnimator;
    public PlayerScore score;
    public int damagePoints;

    public float immunityTime = 1;
    private bool immune = false;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Satellite")
        {
            StartCoroutine(DamageShip());
        }
    }

    private IEnumerator DamageShip()
    {
        if (!immune)
        {
            ApplyDamage();
            immune = true;
        }
        else
        {
            yield return new WaitForSeconds(immunityTime);
            immune = false;
        }
    }

    private void ApplyDamage()
    {
        damageAnimator.SetActive(true);
        score.AddScore(damagePoints);
    }
}
