using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] GameObject healthTextbox;

    private void Start()
    {
        currentHealth = currentMaxHealth;
        SetHealthTextValue();
    }


    public override void ChangeHealth(float damage, GameObject unit)
    {
        if (currentHealth > 0)
        {
            currentHealth += damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, currentMaxHealth);
            SetHealthTextValue();
            Debug.Log("Current health of : " + unit.gameObject.name + " is " + currentHealth);
        }


    }
    private void SetHealthTextValue()
    {
        healthTextbox.GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString();
    }

}
