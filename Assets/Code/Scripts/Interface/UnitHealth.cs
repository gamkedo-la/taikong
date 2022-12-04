using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
  // Fields
  float _currentHealth;
  float _currentMaxHealth;

  // Properties
  public float Health
  {
    get 
    {
      return _currentHealth;
    }
    set 
    {
      _currentHealth = value;
    }
  }

  public float MaxHealth
  {
    get
    {
      return _currentMaxHealth;
    }
    set 
    {
      _currentMaxHealth = value;
    }
  }

  // Constructor
  public UnitHealth(float health, float maxHealth)
  {
    _currentHealth = health;
    _currentMaxHealth = maxHealth;
  }

  // Methods
  public void DamageUnit(float damageAmount)
  {
    _currentHealth -= damageAmount;
  }

  public void HealUnit(float healAmount)
  {
    if (_currentHealth < _currentMaxHealth) 
    {
      _currentHealth += healAmount;
    }

    if (_currentHealth > _currentMaxHealth) 
    {
      _currentHealth = _currentMaxHealth;
    }
  }
}