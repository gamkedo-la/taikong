using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] GameObject healthTextbox;
    [SerializeField] AudioSource damageSound;
    [SerializeField] AudioSource destroySound;

    float shieldTransparency = 0f;

    void Start() 
    {
        SetHealthTextValue();
    }

    void Update()
    {
        //debugging
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     DamagePlayer(10);
        // }
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     HealPlayer(5);
        // }
        if (shieldTransparency > 0f) {
            shieldTransparency -= 1f * Time.deltaTime;
        }
        
        GetComponent<Renderer>().sharedMaterial.SetFloat("_Alpha", shieldTransparency);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("EnemyLaser")) {
            shieldTransparency = 0.5f;
            DamagePlayer(other.GetComponent<Lasers>().laserDamage);
            other.GetComponent<Lasers>().DestroySelf();
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
    }

    private void DamagePlayer(int damage) 
    {
        Debug.Log("Player hit for " + damage.ToString() + " damage");
        GameManager.gameManager.playerHealth.DamageUnit(damage);
        damageSound.Play();

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
        destroySound.Play();
        Destroy(transform.parent.gameObject, 2f);
    }

    private void SetHealthTextValue()
    {
        healthTextbox.GetComponent<UnityEngine.UI.Text>().text = GameManager.gameManager.playerHealth.Health.ToString();
    }
}
