using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : Health
{
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

    private void DamagePlayer(int damage) 
    {
        GameManager.gameManager._playerHealth.DamageUnit(damage);
        Debug.Log(GameManager.gameManager._playerHealth.Health);
    }

    private void HealPlayer(int healing) 
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        Debug.Log(GameManager.gameManager._playerHealth.Health);
    }
}
