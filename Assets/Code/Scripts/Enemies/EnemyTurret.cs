using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    bool playerInRange = false;
    Transform weapon;
    Transform player;

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
}
