using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform target;
    [SerializeField] Transform shootPoint;

    private bool playerInRange;

    float fireRate = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Player has entered the enemy's field of view
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
            transform.LookAt(other.transform);
            Shoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }

    private void Shoot()
    {
        while(playerInRange)
        {
            fireRate -= Time.deltaTime;

            if(fireRate <= 0)
            {
                Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            }
        }

    }
}
