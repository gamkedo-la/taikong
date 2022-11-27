using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void OnResume() {
        Time.timeScale = 1;
        GameManager.currentState = GameManager.GameState.playing;
    }

    public void OnQuit() {
        GameManager.currentState = GameManager.GameState.mainmenu;
        SceneManager.LoadScene(0);
    }
}
