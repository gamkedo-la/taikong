using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour, IEnemyBehaviour
{
    [SerializeField] protected AudioSource fireWeaponSound;
    [SerializeField] AudioSource damageSound;
    [SerializeField] AudioClip destroyedSound;
    [SerializeField] protected Transform weaponLocation;
    [SerializeField] GameObject explosion;
    protected Transform player;

    // bool playerInRange = false;
    protected UnitHealth health = new UnitHealth(50, 50);

    protected enum Status { Idle, Attacking, Dying }
    protected Status currentStatus = Status.Idle;
    public Transform laserPrefab;
    public float firingRate;
    public int scorePoints;

    private void Start() {
        InvokeRepeating("FireWeapon", 1.0f, firingRate);
    }

    private void Update() {
        if (currentStatus == Status.Attacking) {
            transform.LookAt(player.transform.position);
        } else {
            Quaternion weaponLook = Quaternion.LookRotation(weaponLocation.transform.position);
            weaponLocation.rotation = Quaternion.Slerp(weaponLocation.rotation, weaponLook, Time.deltaTime);
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
        if (currentStatus == Status.Attacking) {
            fireWeaponSound.Play();
            Transform laser = Instantiate(laserPrefab);
            // Get the position of the canon part of the turret model
            laser.transform.position = weaponLocation.transform.position;
            laser.transform.LookAt(player);
        }
    }

    public void DestroySelf() 
    {
        // Add points to players score when enemy is destroyed
        ScoreKeeper.instance.AddScore(scorePoints);

        GameObject deathExplosion = Instantiate(explosion);
        deathExplosion.transform.position = transform.position;
        deathExplosion.GetComponent<ParticleSystem>().Play();
        Destroy(deathExplosion, 0.5f);
        transform.position = new Vector3(0, -200, 0);
        damageSound.clip = destroyedSound;
        damageSound.Play();
        Destroy(gameObject, 3f);
    }
}
