using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lasers : MonoBehaviour
{
    public static event Action<float, GameObject> OnDamaged;

    public float laserSpeed = 100;
    public float timeToLive = 5f;
    [SerializeField] float laserDamage = 1f;
    public Rigidbody rb;

    void Start() 
    {
        Destroy(gameObject, timeToLive);
        rb = GetComponent<Rigidbody>();
    }

    void Update() 
    {
        transform.position += transform.forward * laserSpeed * Time.deltaTime;
        
    }

    private void OnEnable()
    {
        Enemy.OnShoot += Shoot;
    }

    private void OnDisable()
    {
        Enemy.OnShoot -= Shoot;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.GetComponent<IDamageable>() != null)
            OnDamaged?.Invoke((laserDamage * -1), other.gameObject);
        Destroy(gameObject);
    }
    */

    private void Shoot(IDamageable damageable)
    {
        GameObject target = damageable.GetTransform().gameObject;
        Vector3 startPosition = transform.position;
        float time = 0;

        while(time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, target.transform.position, time);
            transform.LookAt(target.transform.position + new Vector3(0, 0, 0));

            time += Time.deltaTime * laserSpeed;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<IDamageable>() != null)
            OnDamaged?.Invoke((laserDamage * -1), collision.gameObject);
        Destroy(gameObject);
    }
}
