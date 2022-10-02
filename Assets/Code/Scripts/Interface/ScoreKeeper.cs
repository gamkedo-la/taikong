using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance; // singleton so anyone can add to score without each having a reference

    private int displayScoreDigits = 8;

    private Text scoreDisplay;
    private int scoreNow;

    private void Awake() {
        instance = this;
        scoreDisplay = GetComponent<Text>();
    }

    void Start()
    {
        ResetScore();

    }

    public void ResetScore() {
        scoreNow = 0;
        RedrawDisplay();
    }

    public void AddScore(int extraPoints) {
        scoreNow += extraPoints;
        // if there's a max score this would be where to enforce it
        RedrawDisplay();
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
