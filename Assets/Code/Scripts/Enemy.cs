using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform target;
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
            Shoot();
        }
    }

    //Player has entered the enemy's field of view
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
            target = other.transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerInRange = false;
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
        transform.LookAt(target.transform);
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        fireRate = startFireRate;
    }
}
