using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour, IEnemyBehaviour
{
    bool playerInRange = false;
    UnitHealth health = new UnitHealth(50, 50);
    [SerializeField] Transform weapon;
    Transform player;

    public Transform turretLaser;
    public float firingRate;
    public int scorePoints;

    private void Start() {
        InvokeRepeating("FireWeapon", 1.0f, firingRate);
    }

    private void OnTriggerEnter(Collider other)
    {           
        if(other.CompareTag("PlayerDetect"))
        {
            LockOnPlayer(other.transform);
            // Debug.Log("Looking at object: " + other.transform.ToString());
        }

        // CapsuleCollider is used for enemy hurtboxes
        if(other.CompareTag("PlayerLaser")) {
            health.DamageUnit(other.GetComponent<Lasers>().laserDamage);
        }
    }

    private void OnTriggerExit(Collider other)
    {   
        if(other.CompareTag("PlayerDetect"))
        {
            IgnorePlayer();
        }
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

        if (health.Health <= 0) {
            DestroySelf();
        }
    }

    public void LockOnPlayer(Transform p) 
    {
        playerInRange = true;
        player = p;
    }

    public void IgnorePlayer() 
    {
        playerInRange = false;
    }

    public void FireWeapon() 
    {
        Vector3 barrelOffset = new Vector3(0, 4.2f, 0);

        if (playerInRange) {
            GetComponent<AudioSource>().Play();
            Transform laser = Instantiate(turretLaser);
            // Get the position of the canon part of the turret model
            laser.transform.position = weapon.transform.position + barrelOffset;
            laser.transform.rotation = weapon.transform.rotation;
        }
    }

    public void DestroySelf() 
    {
        // TODO
        // When enemy health gets to zero, trigger an explosion animation
        // then remove the game object from the scene
        Destroy(gameObject);

        // Add points to players score when enemy is destroyed
        ScoreKeeper.instance.AddScore(scorePoints);
    }
}
