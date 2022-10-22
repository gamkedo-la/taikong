using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lasers : MonoBehaviour
{
    public float laserSpeed = 100;
    public int timeToLive = 2;
    public int laserDamage = 5;

    void Start() 
    {
        Destroy(transform.gameObject, timeToLive);
    }

    void Update() 
    {
        transform.position += transform.forward * laserSpeed * Time.deltaTime;
    }
}
