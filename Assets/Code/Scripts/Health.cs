using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//health script for all damageable object
//tutorial video I used: https://www.youtube.com/watch?v=9i0UGVUKiaE&ab_channel=DaniKrossing

public class Health
{
    int currentHealth;
    int currentMaxHealth;

    public int HealthFacts
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

    public int MaxHealth
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

    public Health(int health, int maxHealth)
    {
        currentHealth = health;
        currentMaxHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, currentMaxHealth);
        }
    }

    public void Heal(int heal)
    {
        if (currentHealth > 0)
        {
            currentHealth += heal;
            currentHealth = Mathf.Clamp(currentHealth, 0, currentMaxHealth);
        }
    }
}
