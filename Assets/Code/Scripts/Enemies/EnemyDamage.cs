using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int health = 100;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            DestroySelf();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Hitbox trigger");
        if(other.CompareTag("Laser"))
        {
            health -= 25;
            Debug.Log("Enemy Hit: " + health.ToString());
        }
    }

    private void DestroySelf() {
        // TODO trigger explosion animation effect/particles
        Destroy(transform.parent.gameObject);
    }
}
