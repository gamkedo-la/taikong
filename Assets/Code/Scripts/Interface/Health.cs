using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//health script for all damageable object
//tutorial video I used: https://www.youtube.com/watch?v=9i0UGVUKiaE&ab_channel=DaniKrossing

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public float currentMaxHealth;
    protected float currentHealth;

    public abstract void ChangeHealth(float damage, GameObject unit);

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


    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
