using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lasers : MonoBehaviour
{
    public static event Action<float, GameObject> OnDamaged;

    public float laserSpeed = 100;
    public int timeToLive = 2;
    [SerializeField] float laserDamage = 1f;
    GameObject target;

    void Start() 
    {
        if(transform.parent != null)
            Destroy(transform.parent.gameObject, timeToLive);
        else
            Destroy(transform.gameObject, timeToLive);
    }

    void Update() 
    {
        transform.position += transform.forward * laserSpeed * Time.deltaTime;

        if (target != null)
        {
            transform.LookAt(target.transform.position);
        }

    }

    private void OnEnable()
    {
        Enemy.OnShoot += Shoot;
    }

    private void OnDisable()
    {
        Enemy.OnShoot -= Shoot;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //If this is an enemy bullet, the object being hit isn't an enemy, and the collided object is damagable, then invoke OnDamaged
        if (gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<IDamageable>() != null)
                OnDamaged?.Invoke((laserDamage * -1), other.gameObject);
        }
        //otherwise, it is a player bullet, so check if the collided object is the player,
        //if not, then check if it is damagable, if both are true, then invoke OnDamaged
        else if (!other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<IDamageable>() != null)
        {
            OnDamaged?.Invoke((laserDamage * -1), other.gameObject);
        }
    }

    private void Shoot(IDamageable damageable)
    {
        //if this is an enemy bullet, then set target as the damagable object passed through
        if (gameObject.CompareTag("Enemy"))
            target = damageable.GetTransform().gameObject;
        else
            target = null;
        
        Vector3 startPosition = transform.position;
        float time = 0;

        //shoot at target if it's not null
        while(time < 1 && target != null)
        {
            transform.position = Vector3.Lerp(startPosition, target.transform.position, time);
            transform.LookAt(target.transform.position + new Vector3(0, 0, 0));

            time += Time.deltaTime * laserSpeed;
        }
        

    }
}
