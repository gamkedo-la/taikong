using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//health script for all damageable object
//tutorial video I used: https://www.youtube.com/watch?v=9i0UGVUKiaE&ab_channel=DaniKrossing

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] float currentMaxHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = currentMaxHealth;
    }

    private void OnEnable()
    {
        Lasers.OnDamaged += ChangeHealth;
    }

    private void OnDisable()
    {
        Lasers.OnDamaged -= ChangeHealth;
    }

    public float HealthFacts
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    public float MaxHealth
    {
        get
        {
            return currentMaxHealth;
        }
        set
        {
            currentMaxHealth = value;
        }
    }

    public void ChangeHealth(float damage, GameObject unit)
    {
        if(currentHealth > 0)
        {
            unit.GetComponent<Health>().currentHealth += damage;
            unit.GetComponent<Health>().currentHealth = Mathf.Clamp(currentHealth, 0, currentMaxHealth);
            Debug.Log("Current health of : " + unit.gameObject.name + " is " + unit.GetComponent<Health>().currentHealth);
        }
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
