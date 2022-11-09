using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : EnemyBaseClass
{  
    new public void FireWeapon() 
    {
        Vector3 barrelOffset = new Vector3(0, 8.4f, 0);

        if (currentStatus == Status.Attacking) {
            fireWeaponSound.Play();
            Transform laser = Instantiate(laserPrefab);
            // Get the position of the canon part of the turret model
            laser.transform.position = weaponLocation.transform.position + barrelOffset;
            laser.transform.rotation = weaponLocation.transform.rotation;
        }
    }
}
