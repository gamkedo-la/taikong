using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    public float laserSpeed = 100;
    public float timeToLive = 5f;

    void Start() {
        Destroy(this, timeToLive);
    }

    void Update() {
        transform.position += transform.forward * laserSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this);
    }
}
