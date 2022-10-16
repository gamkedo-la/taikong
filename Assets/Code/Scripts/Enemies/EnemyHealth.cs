using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    // void Update()
    // {
    //     //debugging
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         ChangeHealth(-1, gameObject);
    //     }
    //     if (Input.GetKeyDown(KeyCode.O))
    //     {
    //         ChangeHealth(1, gameObject);
    //     }
    // }
    public override void ChangeHealth(float damage, GameObject unit)
    {
        if (currentHealth > 0)
        {
            currentHealth += damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, currentMaxHealth);
            Debug.Log("Current health of : " + unit.gameObject.name + " is " + currentHealth);
        }
    }
}
