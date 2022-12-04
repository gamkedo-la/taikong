using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lasers : MonoBehaviour
{
    public float laserSpeed = 250;
    public float timeToLive = 2f;
    public float laserDamage = 5f;

    void Start() 
    {
        Destroy(transform.parent.gameObject, timeToLive);
    }

    void Update() 
    {
        switch (GameManager.currentState) {
            case GameManager.GameState.playing:
                transform.position += transform.forward * laserSpeed * Time.deltaTime;
                break;
        }
    }

    // Remove laser from game with particle effect if the laser collides with another game object
    public void DestroySelf() {
        Destroy(transform.parent.gameObject, 0f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Scenary")) {
            DestroySelf();
        }
    }
}
