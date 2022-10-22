using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] GameObject healthTextbox;

    void Start() 
    {
        
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
        if (other.CompareTag("EnemyLaser")) {
            Debug.Log("Player hit");
            DamagePlayer(other.GetComponent<Lasers>().laserDamage);
        }
    }

    private void DamagePlayer(int damage) 
    {
        GameManager.gameManager.playerHealth.DamageUnit(damage);

        if (GameManager.gameManager.playerHealth.Health < 0) {
            DestroyPlayer();
        } else {
            SetHealthTextValue();
        }
        
    }

    private void HealPlayer(int healing) 
    {
        GameManager.gameManager.playerHealth.HealUnit(healing);
        Debug.Log(GameManager.gameManager.playerHealth.Health);
        SetHealthTextValue();
    }

    private void DestroyPlayer() {

    }

    private void SetHealthTextValue()
    {
        healthTextbox.GetComponent<UnityEngine.UI.Text>().text = GameManager.gameManager.playerHealth.Health.ToString();
    }
}
