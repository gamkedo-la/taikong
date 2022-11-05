using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour, IEnemyBehaviour
{
    [SerializeField] AudioSource fireWeaponSound;
    [SerializeField] AudioSource damageSound;
    [SerializeField] AudioClip destroyedSound;
    [SerializeField] Transform weapon;
    [SerializeField] GameObject explosion;
    Transform player;

    // bool playerInRange = false;
    UnitHealth health = new UnitHealth(50, 50);

    enum Status { Idle, Attacking, Dying }
    Status currentStatus = Status.Idle;
    public Transform turretLaser;
    public float firingRate;
    public int scorePoints;

    private void Start() {
        InvokeRepeating("FireWeapon", 1.0f, firingRate);
    }

    private void Update() {
        Vector3 rest = new Vector3(0,0,0);

        if (currentStatus == Status.Attacking) {
            Quaternion weaponLook = Quaternion.LookRotation(player.transform.position - weapon.transform.position);
            weapon.rotation = Quaternion.Slerp(weapon.rotation, weaponLook, Time.deltaTime);
        } else {
            Quaternion weaponLook = Quaternion.LookRotation(weapon.transform.position);
            weapon.rotation = Quaternion.Slerp(weapon.rotation, weaponLook, Time.deltaTime);
        }

        if (health.Health <= 0 && currentStatus != Status.Dying) {
            currentStatus = Status.Dying;
            DestroySelf();
        }
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
            damageSound.Play();
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

    public void LockOnPlayer(Transform p) 
    {
        // playerInRange = true;
        currentStatus = Status.Attacking;
        player = p;
    }

    public void IgnorePlayer() 
    {
        // playerInRange = false;
        currentStatus = Status.Idle;
    }

    public void FireWeapon() 
    {
        Vector3 barrelOffset = new Vector3(0, 4.2f, 0);

        if (currentStatus == Status.Attacking) {
            fireWeaponSound.Play();
            Transform laser = Instantiate(turretLaser);
            // Get the position of the canon part of the turret model
            laser.transform.position = weapon.transform.position + barrelOffset;
            laser.transform.rotation = weapon.transform.rotation;
        }
    }

    public void DestroySelf() 
    {
        // Add points to players score when enemy is destroyed
        ScoreKeeper.instance.AddScore(scorePoints);

        GameObject deathExplosion = Instantiate(explosion);
        deathExplosion.transform.position = transform.position;
        deathExplosion.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
        Destroy(deathExplosion, 0.5f);
        Destroy(gameObject, 1f);
    }
}
