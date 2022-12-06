using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Slider aimSpeedSlider;

    public void InitialAimSettings() {
        Debug.Log(aimSpeedSlider.value);
        PlayerPrefs.SetFloat("SENSITIVITY", aimSpeedSlider.value);
        player.GetComponent<PlayerInputs>().SetupAimSpeed();
    }

    public void StartGame() {
        SceneManager.LoadScene(2);
    }
}
