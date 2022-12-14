using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrunt : EnemyBaseClass
{   
    // The speed at which the ship floats up and down
    [SerializeField] float floatingAmplitude;
    [SerializeField] float floatingSpeed;
    Vector3 originalPos;
    float weaponOffset = 3.7f;
    new UnitHealth health = new UnitHealth(30, 30);
    
    private void Start() {
        InvokeRepeating("FireWeapon", 1.0f, firingRate * Random.Range(0.9f, 1.1f));
        originalPos = transform.position;
    }

    private void Update() {
        Vector3 rest = new Vector3(0,0,0);

        if (currentStatus == Status.Dying) {
            Debug.Log("Grunt destroyed");
            DestroySelf();
        } else {
            if (player != null) {
                Quaternion weaponLook = Quaternion.LookRotation(player.transform.position - weaponLocation.transform.position);
                weaponLocation.rotation = Quaternion.Slerp(weaponLocation.rotation, weaponLook, Time.deltaTime);
            } else {
                Quaternion weaponLook = Quaternion.LookRotation(weaponLocation.transform.position);
                weaponLocation.rotation = Quaternion.Slerp(weaponLocation.rotation, weaponLook, Time.deltaTime);
            }
            transform.position = new Vector3(originalPos.x, originalPos.y + floatingAmplitude * Mathf.Sin(floatingSpeed * Time.time), originalPos.z);
        }
    }

    new private void FireWeapon() 
    {   
        Vector3 barrelOffset = new Vector3(weaponOffset, 2f, -3f);
        weaponOffset *= -1;

        if (currentStatus == Status.Attacking) {
            fireWeaponSound.Play();
            Transform laser = Instantiate(laserPrefab);
            // Get the position of the canon part of the turret model
            laser.transform.position = weaponLocation.transform.position + barrelOffset;
            laser.transform.rotation = weaponLocation.transform.rotation;
        }
    }
}
