using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject healthTextbox;

    private void Start()
    {
        GameManager.gameManager.playerHealth.Health = PlayerPrefs.GetFloat("CURRENT_HEALTH");
        SetHealthTextValue();
    }


    public void ChangeHealth(int damage, GameObject unit)
    {
        if (GameManager.gameManager.playerHealth.Health > 0)
        {
            GameManager.gameManager.playerHealth.Health += damage;
            GameManager.gameManager.playerHealth.Health = Mathf.Clamp(
                GameManager.gameManager.playerHealth.Health, 0, 
                GameManager.gameManager.playerHealth.MaxHealth);

            SetHealthTextValue();
            PlayerPrefs.SetFloat("CURRENT_HEALTH", GameManager.gameManager.playerHealth.Health);

            // Debug.Log("Current health of : " + unit.gameObject.name + " is " + GameManager.gameManager.playerHealth.Health);
        }
    }
    private void SetHealthTextValue()
    {
        healthTextbox.GetComponent<UnityEngine.UI.Text>().text = GameManager.gameManager.playerHealth.Health.ToString();
    }

}
