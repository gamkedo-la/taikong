using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBaseClass
{

    [SerializeField] Transform[] weaponLocations;
    int activeWeapon = 0;
    bool evading = false;
    Vector3 newPosition = new Vector3(0,0,0);
    

    private void Update() {
        if (currentStatus == Status.Dying) {
            DestroySelf();
        } else {
            if (currentStatus == Status.Attacking) {
                foreach (Transform weapon in weaponLocations) {
                    weapon.LookAt(player.transform.position);
                }
            }

            EvadePlayer();
        }
    }

    private void EvadePlayer() {
        if (evading) {
            // Chabuduo...
            if (Mathf.Abs(transform.GetChild(0).localPosition.y - newPosition.y) < 5) {
                evading = false;
            } else {
                transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, newPosition, 0.25f * Time.fixedDeltaTime);
            }
        } else {
            newPosition = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);
            evading = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {           
        if(other.CompareTag("PlayerDetect"))
        {
            LockOnPlayer(other.transform);
        }

        // CapsuleCollider is used for enemy hurtboxes
        if(other.CompareTag("PlayerLaser")) {
            damageSound.Play();
            // Player attack only does a smaller % of the damage to the boss ship
            health.DamageUnit(other.GetComponent<Lasers>().laserDamage / 10);
            GameObject sparks = Instantiate(laserExplosion, transform.GetChild(0));
            Destroy(sparks, 1f);
            if (health.Health <= 0) {
                currentStatus = Status.Dying;
            }
        }
    }

    public void FireWeapon() 
    {
        activeWeapon += 1;
        activeWeapon = activeWeapon % 4;

        if (currentStatus == Status.Attacking) {
            fireWeaponSound.Play();
            Transform laser = Instantiate(laserPrefab);
            // Get the position of the canon part of the turret model
            laser.transform.position = weaponLocations[activeWeapon].transform.position;
            laser.transform.LookAt(player);
        }
    }
}
