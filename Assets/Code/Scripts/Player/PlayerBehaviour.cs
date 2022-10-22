using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    private GameObject healthTextbox;

    void Start() 
    {
        healthTextbox = GameObject.Find("HealthValue");
        SetHealthTextValue();
    }

    void Update()
    {
        //debugging
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DamagePlayer(10);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            HealPlayer(5);
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        Debug.Log(gameObject.GetComponentInChildren<Collider>().name);
    }

    private void DamagePlayer(int damage) 
    {
        GameManager.gameManager.playerHealth.DamageUnit(damage);
        Debug.Log(GameManager.gameManager.playerHealth.Health);
        SetHealthTextValue();
    }

    private void HealPlayer(int healing) 
    {
        GameManager.gameManager.playerHealth.HealUnit(healing);
        Debug.Log(GameManager.gameManager.playerHealth.Health);
        SetHealthTextValue();
    }

    private void SetHealthTextValue()
    {
        healthTextbox.GetComponent<UnityEngine.UI.Text>().text = GameManager.gameManager.playerHealth.Health.ToString();
    }
}
