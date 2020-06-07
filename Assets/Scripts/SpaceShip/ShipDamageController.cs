using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageController : MonoBehaviour
{
    public GameObject damageAnimator;
    public PlayerScore score;
    public int damagePoints = -1250;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Satellite")
        {
            DamageShip();
        }
    }

    private void DamageShip()
    {
        damageAnimator.SetActive(true);
        score.AddScore(damagePoints);
    }
}
