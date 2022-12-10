using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : EnemyBaseClass
{  
    new UnitHealth health = new UnitHealth(40, 40);
    
    private void Update() {
        if (currentStatus == Status.Dying) {
            Debug.Log("Turret destroyed");
            DestroySelf();
        } else {
            if (currentStatus == Status.Attacking) {
                Quaternion weaponLook = Quaternion.LookRotation(player.transform.position - weaponLocation.transform.position);
                weaponLocation.rotation = Quaternion.Slerp(weaponLocation.rotation, weaponLook, Time.deltaTime);
            } else {
                Quaternion weaponLook = Quaternion.LookRotation(weaponLocation.transform.position);
                weaponLocation.rotation = Quaternion.Slerp(weaponLocation.rotation, weaponLook, Time.deltaTime);
            }
        }
    }
    
    new public void FireWeapon() 
    {
        Vector3 barrelOffset = new Vector3(0, 8.4f, 0);
        if (currentStatus == Status.Attacking && player != null) {
            fireWeaponSound.Play();
            Transform laser = Instantiate(laserPrefab);
            // Get the position of the canon part of the turret model
            laser.transform.position = weaponLocation.transform.position + barrelOffset;
            laser.transform.rotation = weaponLocation.transform.rotation;
        }
    }
}
