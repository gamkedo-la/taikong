using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
            Debug.Log(GameManager.gameManager.playerHealth.HealthFacts);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(20);
            Debug.Log(GameManager.gameManager.playerHealth.HealthFacts);
        }
    }

    public Transform GetTransform()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        GameManager.gameManager.playerHealth.TakeDamage(damage);
    }

    public void Heal(int heal)
    {
        GameManager.gameManager.playerHealth.Heal(heal);
    }
}
