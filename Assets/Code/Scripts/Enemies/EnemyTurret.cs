using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    bool playerInRange = false;
    Transform weapon;
    Transform player;
    List<Transform> turretFire;

    public Transform turretLaser;
    public float firingRate;

    private void OnTriggerEnter(Collider other)
    {   //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {   //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Start() {
        weapon = this.gameObject.transform.GetChild(0).GetChild(0).transform;
        InvokeRepeating("CreateLaser", 1.0f, firingRate);
    }

    private void Update() {
        Vector3 rest = new Vector3(0,0,0);

        if (playerInRange) {
            Quaternion weaponLook = Quaternion.LookRotation(player.transform.position - weapon.transform.position);
            weapon.rotation = Quaternion.Slerp(weapon.rotation, weaponLook, Time.deltaTime);
        } else {
            Quaternion weaponLook = Quaternion.LookRotation(weapon.transform.position);
            weapon.rotation = Quaternion.Slerp(weapon.rotation, weaponLook, Time.deltaTime);
        }
    }

    private void CreateLaser() 
    {   
        Vector3 barrelOffset = new Vector3(0, 4.2f, 0);
        if (playerInRange) {
            Transform laser = Instantiate(turretLaser);
            // Get the position of the canon part of the turret model
            laser.transform.position = weapon.transform.position + barrelOffset;
            laser.transform.rotation = weapon.transform.rotation;
        }
    }
}
