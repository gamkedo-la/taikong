using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

    public void ShareScore() {
        // TODO share high-score button
    }

    public void ViewCredits() {
        // TODO link to credits scene
    }
}