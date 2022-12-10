using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] GameObject healthTextbox;
    [SerializeField] AudioSource damageSound;
    [SerializeField] AudioSource destroySound;
    [SerializeField] GameObject deathExplosion;

    float shieldTransparency = 0f;

    void Start() 
    {
        GameManager.currentState = GameManager.GameState.playing;
        SetHealthTextValue();
    }

    void Update()
    {
        switch (GameManager.currentState) {
            case GameManager.GameState.playing:
                if (shieldTransparency > 0f) {
                    shieldTransparency -= 1f * Time.deltaTime;
                }
                
                GetComponent<Renderer>().sharedMaterial.SetFloat("_Alpha", shieldTransparency);
                break;
            
            case GameManager.GameState.dying:
                DestroyPlayer();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(GameManager.currentState) {
            case GameManager.GameState.playing:
                if (other.CompareTag("EnemyLaser")) {
                    shieldTransparency = 0.5f;
                    DamagePlayer(other.GetComponent<Lasers>().laserDamage);
                    other.GetComponent<Lasers>().DestroySelf();
                    transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                }

                if (other.CompareTag("ShieldPickup")) {
                    shieldTransparency = 1f;
                    HealPlayer(50);
                }
                break;
        } 
        
    }

    private void DamagePlayer(float damage) 
    {
        GameManager.gameManager.playerHealth.DamageUnit(damage);
        damageSound.Play();

        if (GameManager.gameManager.playerHealth.Health < 0) {
            GameManager.currentState = GameManager.GameState.dying;
        } else {
            SetHealthTextValue();
        }
        
    }

    private void HealPlayer(float healing) 
    {
        GameManager.gameManager.playerHealth.HealUnit(healing);
        Debug.Log(GameManager.gameManager.playerHealth.Health);
        SetHealthTextValue();
    }

    private void DestroyPlayer() {
        GameObject playerExplostion = Instantiate(deathExplosion);
        playerExplostion.transform.position = transform.position;
        playerExplostion.GetComponent<ParticleSystem>().Play();
        Destroy(playerExplostion, 0.5f);
        transform.parent.transform.position = new Vector3(0, -200, 0);
        destroySound.Play();
        GameManager.currentState = GameManager.GameState.gameover;
        StartCoroutine(LoadGameOver(5f));
        SceneManager.LoadScene(4);
    }

    IEnumerator LoadGameOver(float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(4);
    }

    private void SetHealthTextValue()
    {
        healthTextbox.GetComponent<UnityEngine.UI.Text>().text = GameManager.gameManager.playerHealth.Health.ToString();
    }
}
