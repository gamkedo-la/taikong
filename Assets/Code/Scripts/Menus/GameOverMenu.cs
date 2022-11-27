using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverMenu : MonoBehaviour
{
    [Header("Game Over Menu")]
    [SerializeField] GameObject scoreTextbox;
    [SerializeField] GameObject clipboardPrompt;

    private int finalScore;
    void Start() {
        finalScore = PlayerPrefs.GetInt("playerscore");
        scoreTextbox.GetComponent<Text>().text = finalScore.ToString();
    }
    public void OnMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void OnShareScore() {
        string shareText = "I got a high score of " + finalScore.ToString() + " on Taikong, think you can beat it? https://jazibobs.itch.io/taikong";
        GUIUtility.systemCopyBuffer = shareText;
        clipboardPrompt.SetActive(true);
    }
}