using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static event Action<IDamageable> OnShoot;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject target;
    [SerializeField] Transform shootPoint;
    [SerializeField] float startFireRate = 1f;

    private bool playerInRange;
    private float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
        fireRate = startFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireRate -= Time.deltaTime;
        if (playerInRange && fireRate <= 0)
        {
            //Shoot();
        }
    }
    private void OnEnable()
    {
        AttackRadius.OnAttack += Shoot;
    }

    private void OnDisable()
    {
        AttackRadius.OnAttack -= Shoot;
    }


    /*
    //Player has entered the enemy's field of view
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
            target = other.gameObject;
            Debug.Log("Attacking player");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerInRange = false;
    }
    */

    private void Shoot(IDamageable damageable)
    {
        Debug.Log("Shoot");
        transform.LookAt(damageable.GetTransform());
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        OnShoot?.Invoke(damageable);
        fireRate = startFireRate;
        //Destroy(newBullet, 5f);
    }
}
