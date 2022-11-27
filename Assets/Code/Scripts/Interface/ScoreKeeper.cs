using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance; // singleton so anyone can add to score without each having a reference
    private Scene activeScene;
    private int displayScoreDigits = 8;

    private Text scoreDisplay;
    private int scoreNow;

    private void Awake() {
        instance = this;
        scoreDisplay = GetComponent<Text>();
        
    }

    private void Start() {
        activeScene = SceneManager.GetActiveScene();
        if (activeScene.buildIndex == 0) {
            ResetScore();
        } else {
            scoreNow = PlayerPrefs.GetInt("playerscore");
            RedrawDisplay();
        }
    }

    public void ResetScore() {
        scoreNow = 0;
        PlayerPrefs.SetInt("playerscore", scoreNow);
        RedrawDisplay();
    }

    public void AddScore(int extraPoints) {
        scoreNow += extraPoints;
        PlayerPrefs.SetInt("playerscore", scoreNow);
        RedrawDisplay();

        if (scoreNow > PlayerPrefs.GetInt("highscore")) {
            PlayerPrefs.SetInt("highscore", scoreNow);
        }
    }

    void RedrawDisplay() {
        if(scoreDisplay) {
            string scoreAsText = "" + scoreNow; // to get length for padding zeros, and will be string for UI anyway
            while(scoreAsText.Length<displayScoreDigits) {
                scoreAsText = "0" + scoreAsText;
            }
            scoreDisplay.text = scoreAsText;
        } else {
            Debug.LogWarning("Score unable to display, missing canvas Text on same GO as this component");
        }
    }
}
