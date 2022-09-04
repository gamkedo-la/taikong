using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    public float laserSpeed = 50;
    public float timeToLive = 5f;

    void Start() {
        Destroy(gameObject, timeToLive);
    }

    void Update() {
        transform.position += transform.forward * laserSpeed * Time.deltaTime;
    }
}